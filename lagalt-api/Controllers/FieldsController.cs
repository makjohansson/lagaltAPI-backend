using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.FieldDTOs;
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
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FieldsController : ControllerBase
    {
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;

        public FieldsController(LagaltDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a specific field by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FieldReadDTO>> GetById (int id)
        {
            if (!Exists(id))
            {
                return NotFound($"The field with the id {id} was not found");
            }
            var field = await _context.Fields.FirstOrDefaultAsync(f => f.FieldId == id);

            return _mapper.Map<FieldReadDTO>(field);
        }

        /// <summary>
        /// Get all fields
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FieldReadDTO>>> GetAll ()
        {
            return _mapper.Map<List<FieldReadDTO>>(await _context.Fields
                .ToListAsync());
        }

        /// <summary>
        /// Create a field
        /// </summary>
        /// <param name="fieldDto">data for the new field</param>
        /// <returns>the created field</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Field>> Add (FieldCreateDTO fieldDto)
        {
            Field field = _mapper.Map<Field>(fieldDto);
            _context.Fields.Add(field);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = field.FieldId }, _mapper.Map<FieldCreateDTO>(field));
        }

        /// <summary>
        /// Check if a specific field exists
        /// </summary>
        /// <param name="id">field id</param>
        /// <returns></returns>
        private bool Exists(int id)
        {
            return _context.Fields.Any(f => f.FieldId == id);
        }
    }
}
