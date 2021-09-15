using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.FieldProjectDTOs;
using lagalt_api.Models.DTOs.KeywordProjectCreateDTO;
using lagalt_api.Models.DTOs.ProjectDTOs;
using lagalt_api.Models.DTOs.ProjectUsersDTOs;
using lagalt_api.Models.DTOs.SkillProjectDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace lagalt_api.Controllers
{
    [Route("projects")]
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
        public async Task<ActionResult<IEnumerable<ProjectReadDTO>>> GetAllProjects()
        {
            return _mapper.Map<List<ProjectReadDTO>>(await _context.Projects
                .Include(p => p.Skills)
                .Include(p => p.Fields)
                .Include(p => p.ProjectUsers)
                .Include(p => p.Keywords)
                .Include(p => p.Photos)
                .Include(p => p.Messages)
                .ToListAsync());
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


        [HttpGet("{keywordId}/keyword")]
        public async Task<ActionResult<IEnumerable<ProjectReadDTO>>> GetProjectsByKeywords(int keywordId)
        {

            return _mapper.Map<List<ProjectReadDTO>>(await _context.Keywords
               .Where(k => k.KeywordId == keywordId)
                .SelectMany(k => k.Projects)
                .Include(p => p.Skills)
                .Include(p => p.Fields)
                .Include(p => p.ProjectUsers)
                .Include(p => p.Keywords)
                .Include(p => p.Photos)
                .Include(p => p.Messages)
                .ToListAsync()) ;
        }
        /// <summary>
        /// Adds a new project to the database
        /// </summary>
        /// <param name="projectDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<User>> AddProject(ProjectCreateDTO projectDto)
        {
            Project project = _mapper.Map<Project>(projectDto);
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectById", new { id = project.ProjectId }, _mapper.Map<ProjectCreateDTO>(project));
        }

        [HttpPut("{projectId}/keyword/{keywordId}")]
        public async Task<ActionResult> AddKeywordToProject(int projectId, int keywordId, KeywordProjectCreateDTO keywordProjectIds)
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

            return CreatedAtAction("AddKeywordToProject", keywordProjectIds);
        }

        [HttpPut("{projectId}/field/{fieldId}")]
        public async Task<ActionResult> AddFieldToProject(int projectId, int fieldId, FieldProjectCreateDTO fieldProjectIds)
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

            return CreatedAtAction("AddFieldToProject", fieldProjectIds);
        }

        [HttpPut("{projectId}/skill/{skillId}")]
        public async Task<ActionResult> AddSkillToProject(int projectId, int skillId, SkillProjectCreateDTO skillProjectIds)
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

            return CreatedAtAction("AddSkillToProject", skillProjectIds);
        }
    }
}
