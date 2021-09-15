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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SkillDTO>> GetById(int id)
        {
            if (!Exists(id))
            {
                return NotFound($"The skill with the id {id} was not found");
            }
            var skill = await _context.Skills.FirstOrDefaultAsync(f => f.SkillId == id);

            return _mapper.Map<SkillDTO>(skill);

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SkillDTO>>> GetAll()
        {
            return _mapper.Map<List<SkillDTO>>(await _context.Skills
                .ToListAsync());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Skill>> AddSkill(SkillDTO skillDto)
        {
            Skill skill = _mapper.Map<Skill>(skillDto);
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = skill.SkillId }, _mapper.Map<SkillDTO>(skill));
        }

        private bool Exists(int id)
        {
            return _context.Skills.Any(s => s.SkillId == id);
        }
    }
}
