using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.ApplicationDTOs;
using lagalt_api.Models.DTOs.FieldProjectDTOs;
using lagalt_api.Models.DTOs.KeywordProjectCreateDTO;
using lagalt_api.Models.DTOs.MessagesDTOs;
using lagalt_api.Models.DTOs.ProjectDTOs;
using lagalt_api.Models.DTOs.SkillProjectDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace lagalt_api.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProjectsController : ControllerBase
    {
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;

        public ProjectsController(LagaltDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <param name="projectParameters">parameters to get a specific number of projects per page, and the number of the page</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ProjectParamReadDTO>> GetAll([FromQuery] ProjectParameters projectParameters)
        {
            var projectParam = new ProjectParamReadDTO
            {
                Count = _context.Projects.Count(),
                PageNumber = projectParameters.PageNumber,
                PageSize = projectParameters.PageSize,
                Projects = _mapper
                .Map<List<ProjectReadDTO>>(await _context.Projects
                .OrderBy(p => p.ProjectId)
                .Skip((projectParameters.PageNumber - 1) * projectParameters.PageSize)
                .Take(projectParameters.PageSize)
                .Include(p => p.Skills)
                .Include(p => p.Fields)
                .Include(p => p.Keywords)
                .Include(p => p.Photos)
                .ToListAsync())
            };

            return projectParam;
        }

        /// <summary>
        /// Get a specific project by id
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns></returns>
        [HttpGet("{id}/project")]
        public async Task<ActionResult<ProjectReadDTO>> GetById(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Skills)
                .Include(p => p.Fields)
                .Include(p => p.ProjectUsers)
                .Include(p => p.Keywords)
                .Include(p => p.Photos)
                .Include(p => p.Messages)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }
            return _mapper.Map<ProjectReadDTO>(project);
        }

        /// <summary>
        /// Get all projects that include the specific field and keywords
        /// </summary>
        /// <param name="keywords">string of keyword ids, seperated with ","</param>
        /// <param name="field">field name</param>
        /// <returns></returns>
        [HttpGet("keywords/field")]
        public async Task<ActionResult<IEnumerable<ProjectReadDTO>>> GetProjectsByKeywordsAndField(string keywords, string field)
        {
            var idArray = Array.ConvertAll(keywords.Split(","), int.Parse);

            var projectList = _mapper.Map<List<ProjectReadDTO>>(await _context.Keywords
               .Where(k => idArray.Contains(k.KeywordId))
                .SelectMany(k => k.Projects)
                .Include(p => p.Skills)
                .Include(p => p.Fields)
                .Include(p => p.ProjectUsers)
                .Include(p => p.Keywords)
                .Include(p => p.Photos)
                .Include(p => p.Messages)  
                .ToListAsync());

            var test = projectList.FindAll(p => p.Fields.Contains(field));
            
            var distinctProjectList = test.GroupBy(p => p.ProjectId).Select(p => p.First()).ToList();

            return distinctProjectList;
        }

        /// <summary>
        /// Get all messages from a specific project
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns></returns>
        [HttpGet("{projectId}/messages")]
        public async Task<ActionResult<IEnumerable<MessageReadDTO>>> GetMessagesByProjectId(int projectId)
        {
            return _mapper.Map<List<MessageReadDTO>>(await _context.Projects
               .Where(p => p.ProjectId == projectId)
                .SelectMany(p => p.Messages)
                .ToListAsync());
        }

        /// <summary>
        /// Get all applications from a specific project
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns></returns>
        [HttpGet("{projectId}/applications")]
        public async Task<ActionResult<IEnumerable<ApplicationReadDTO>>> GetApplicationsByProjectId(int projectId)
        {
            return _mapper.Map<List<ApplicationReadDTO>>(await _context.Projects
               .Where(p => p.ProjectId == projectId)
                .SelectMany(p => p.Applications)
                .ToListAsync());
        } 

        /// <summary>
        /// Create a project
        /// </summary>
        /// <param name="userId">id of the user that creates the project</param>
        /// <param name="projectDto">data for the new application</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<User>> Add(string userId, ProjectCreateDTO projectDto)
        {
            Project project = _mapper.Map<Project>(projectDto);
            _context.Projects.Add(project);

            await _context.SaveChangesAsync();
            await AddUserToProject(project.ProjectId, userId, true);

            return CreatedAtAction("GetProjectById", new { id = project.ProjectId }, _mapper.Map<ProjectCreateDTO>(project));
        }

        /// <summary>
        /// Edit a project
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="projectDTO">data to change for the project</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Edit(int projectId, ProjectEditDTO projectDTO)
        {
            Project project = _context.Projects.Find(projectId);
            if(project == null)
            {
                return NotFound($"No project with id {projectId} was found");
            }

            project.ProjectName = projectDTO.ProjectName;
            project.Description = projectDTO.Description;
            project.UrlReference = projectDTO.UrlReference;
            project.Progress = projectDTO.Progress;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Close a project
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <returns></returns>
        [HttpPut("close")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CloseProject(int projectId)
        {
            Project project = _context.Projects.Find(projectId);
            if (project == null)
            {
                return NotFound($"No project with id {projectId} was found");
            }

            project.Progress = Models.Domain.Enums.ProgressStatus.Completed;
            project.Closed = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a keyword to a project
        /// </summary>
        /// <param name="keywordProjectIds">keyword id and project id</param>
        /// <returns></returns>
        [HttpPut("keyword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddKeywordToProject(KeywordProjectCreateDTO keywordProjectIds)
        {
            Project project = _context.Projects.Include("Keywords").First(p => p.ProjectId == keywordProjectIds.ProjectId);
            project.Keywords.Add(_context.Keywords.Find(keywordProjectIds.KeywordId));
            try
            {
                await _context.SaveChangesAsync();
            }

            catch
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Add a field to a project
        /// </summary>
        /// <param name="fieldProjectIds">field id and project id</param>
        /// <returns></returns>
        [HttpPut("field")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddFieldToProject(FieldProjectCreateDTO fieldProjectIds)
        {
            Project project = _context.Projects.Include("Fields").First(p => p.ProjectId == fieldProjectIds.ProjectId);
            project.Fields.Add(_context.Fields.Find(fieldProjectIds.FieldId));
            try
            {
                await _context.SaveChangesAsync();
            }

            catch
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Add a skill to a project
        /// </summary>
        /// <param name="skillProjectIds">skill id and project id</param>
        /// <returns></returns>
        [HttpPut("skill")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddSkillToProject(SkillProjectCreateDTO skillProjectIds)
        {
            Project project = _context.Projects.Include("Skills").First(p => p.ProjectId == skillProjectIds.ProjectId);
            project.Skills.Add(_context.Skills.Find(skillProjectIds.SkillId));
            try
            {
                await _context.SaveChangesAsync();
            }

            catch
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Add a user to a project
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="userId">user id</param>
        /// <param name="owner">boolean</param>
        /// <returns></returns>
        [HttpPut("{projectId}/user/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddUserToProject(int projectId, string userId, bool owner)
        {
            _context.ProjectUsers.Add( new() { ProjectId = projectId, UserId = userId, Owner = owner });

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete a project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            Project project = await _context.Projects.FindAsync(id);

            if (!Exists(id))
            {
                return NotFound();
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Check if a specific project exists
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns></returns>
        private bool Exists(int id)
        {
            return _context.Projects.Any(p => p.ProjectId == id);
        }
    }
}
