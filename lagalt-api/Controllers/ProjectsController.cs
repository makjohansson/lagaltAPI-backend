using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.ApplicationDTOs;
using lagalt_api.Models.DTOs.FieldProjectDTOs;
using lagalt_api.Models.DTOs.KeywordProjectCreateDTO;
using lagalt_api.Models.DTOs.MessagesDTOs;
using lagalt_api.Models.DTOs.ProjectDTOs;
using lagalt_api.Models.DTOs.ProjectUsersDTOs;
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
        /// Gets all projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ProjectParamReadDTO>> GetAllProjects([FromQuery] ProjectParameters projectParameters)
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

        [HttpGet("{id}/project")]
        public async Task<ActionResult<ProjectReadDTO>> GetProjectById(int id)
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


        [HttpGet("keywords/field")]
        public async Task<ActionResult<IEnumerable<ProjectReadDTO>>> GetProjectsByKeywords(string keywords, string fields)
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

            var test = projectList.FindAll(p => p.Fields.Contains(fields));
            

            var distinctProjectList = test.GroupBy(p => p.ProjectId).Select(p => p.First()).ToList();


            return distinctProjectList;
        }

        [HttpGet("{projectId}/messages")]
        public async Task<ActionResult<IEnumerable<MessageReadDTO>>> GetMessagesByProjectId(int projectId)
        {

            return _mapper.Map<List<MessageReadDTO>>(await _context.Projects
               .Where(p => p.ProjectId == projectId)
                .SelectMany(p => p.Messages)
                .ToListAsync());
        }

        [HttpGet("{projectId}/applications")]
        public async Task<ActionResult<IEnumerable<ApplicationReadDTO>>> GetApplicationsByProjectId(int projectId)
        {
            return _mapper.Map<List<ApplicationReadDTO>>(await _context.Projects
               .Where(p => p.ProjectId == projectId)
                .SelectMany(p => p.Applications)
                .ToListAsync());
        } 

        /// <summary>
        /// Adds a new project to the database
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<User>> AddProject(string userId, ProjectCreateDTO projectDto)
        {
            Project project = _mapper.Map<Project>(projectDto);
            _context.Projects.Add(project);

            await _context.SaveChangesAsync();
            await AddUserToProject(project.ProjectId, userId, true);

            return CreatedAtAction("GetProjectById", new { id = project.ProjectId }, _mapper.Map<ProjectCreateDTO>(project));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> EditProject(int projectId, ProjectEditDTO projectDTO)
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

        [HttpPut("{projectId}/user/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddUserToProject(int projectId, string userId, bool owner)
        {
            _context.ProjectUsers.Add( new() { ProjectId = projectId, UserId = userId, Owner = owner });

            await _context.SaveChangesAsync();

            return NoContent();
        }

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

        private bool Exists(int id)
        {
            return _context.Projects.Any(p => p.ProjectId == id);
        }


    }
}
