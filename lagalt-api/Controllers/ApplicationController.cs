using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.ApplicationDTOs;
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

        /// <summary>
        /// Get a specific application by id
        /// </summary>
        /// <param name="id">application id</param>
        /// <returns>ApplicationReadDTO</returns>
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

        /// <summary>
        /// Create an application
        /// </summary>
        /// <param name="applicationDto">data for the new application</param>
        /// <returns>The created application</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Application>> Add(ApplicationCreateDTO applicationDto)
        {
            Application application = _mapper.Map<Application>(applicationDto);
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = application.ApplicationId }, _mapper.Map<ApplicationCreateDTO>(application));
        }  

        /// <summary>
        /// Approve an application
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <param name="ownerId">owner id</param>
        /// <returns></returns>
        [HttpPut("approve")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> ApproveApplication(int applicationId, string ownerId)
        {
            Application application = await _context.Applications.Where(a => a.ApplicationId == applicationId).FirstOrDefaultAsync();
            if (application == null)
            {
                return NotFound();
            }

            application.Approved = true;
            application.ApprovedByOwnerId = ownerId;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        /// <summary>
        /// Deny an application
        /// </summary>
        /// <param name="applicationId">application id</param>
        /// <returns></returns>
        [HttpPut("deny")]
        public async Task<ActionResult> DenyApplication(int applicationId)
        {
            Application application = await _context.Applications.Where(a => a.ApplicationId == applicationId).FirstOrDefaultAsync();
            if (application == null)
            {
                return NotFound();
            }

            application.Approved = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Check if a specific application exists
        /// </summary>
        /// <param name="id">application id</param>
        /// <returns></returns>
        private bool Exists(int id)
        {
            return _context.Applications.Any(a => a.ApplicationId == id);
        }
    }
}
