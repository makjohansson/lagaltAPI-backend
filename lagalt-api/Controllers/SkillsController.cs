using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.SkillDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace lagalt_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class SkillsController : ControllerBase
    {
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;

        public SkillsController(LagaltDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a specific skill by id
        /// </summary>
        /// <param name="id">skill id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SkillReadDTO>> GetById(int id)
        {
            if (!Exists(id))
            {
                return NotFound($"The skill with the id {id} was not found");
            }
            var skill = await _context.Skills.FirstOrDefaultAsync(f => f.SkillId == id);

            return _mapper.Map<SkillReadDTO>(skill);
        }

        /// <summary>
        /// Get all skills
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SkillReadDTO>>> GetAll()
        {
            return _mapper.Map<List<SkillReadDTO>>(await _context.Skills
                .ToListAsync());
        }

        /// <summary>
        /// Create a skill
        /// </summary>
        /// <param name="skillDto">data for the new skill</param>
        /// <returns>the created skill</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Skill>> Add(SkillCreateDTO skillDto)
        {
            Skill skill = _mapper.Map<Skill>(skillDto);
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = skill.SkillId }, _mapper.Map<SkillCreateDTO>(skill));
        }

        /// <summary>
        /// Check if a specific skill exists
        /// </summary>
        /// <param name="id">skill id</param>
        /// <returns></returns>
        private bool Exists(int id)
        {
            return _context.Skills.Any(s => s.SkillId == id);
        }
    }
}
