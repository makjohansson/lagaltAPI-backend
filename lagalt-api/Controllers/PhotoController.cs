using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.PhotoDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace lagalt_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class PhotoController : ControllerBase
    {
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;

        public PhotoController(LagaltDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a specific photo by id
        /// </summary>
        /// <param name="id">photo id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PhotoReadDTO>> GetById(int id)
        {
            if (!Exists(id))
            {
                return NotFound($"The photo with the id {id} was not found");
            }
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.PhotoId == id);

            return _mapper.Map<PhotoReadDTO>(photo);
        }

        /// <summary>
        /// Create a photo
        /// </summary>
        /// <param name="photoDto">data for the new photo</param>
        /// <returns>The created photo</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Photo>> Add(PhotoCreateDTO photoDto)
        {
            Photo photo = _mapper.Map<Photo>(photoDto);
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = photo.PhotoId }, _mapper.Map<PhotoCreateDTO>(photo));
        }

        /// <summary>
        /// Check if a specific photo exists
        /// </summary>
        /// <param name="id">photo id</param>
        /// <returns></returns>
        private bool Exists(int id)
        {
            return _context.Photos.Any(p => p.PhotoId == id);
        }
    }
}
