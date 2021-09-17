using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.ApplicationDTOs;
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
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ApplicationController : ControllerBase
    {
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;

        public ApplicationController(LagaltDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApplicationReadDTO>> GetById(int id)
        {
            if (!Exists(id))
            {
                return NotFound($"The application with the id {id} was not found");
            }
            var application = await _context.Applications.FirstOrDefaultAsync(a => a.ApplicationId == id);

            return _mapper.Map<ApplicationReadDTO>(application);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Application>> AddPortfolio(ApplicationCreateDTO applicationDto)
        {
            Application application = _mapper.Map<Application>(applicationDto);
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = application.ApplicationId }, _mapper.Map<ApplicationCreateDTO>(application));
        }



        private bool Exists(int id)
        {
            return _context.Applications.Any(a => a.ApplicationId == id);
        }

    }
}
