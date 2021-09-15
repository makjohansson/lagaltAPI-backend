using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.DTOs.PhotoDTOs;
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
    [Route("[controller]")]
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
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PhotoDTO>> GetById(int id)
        {
            if (!Exists(id))
            {
                return NotFound($"The photo with the id {id} was not found");
            }
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.PhotoId == id);

            return _mapper.Map<PhotoDTO>(photo);

        }
        private bool Exists(int id)
        {
            return _context.Photos.Any(p => p.PhotoId == id);
        }
    }
}
