
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lagalt_api.Data;
using lagalt_api.Models.DTOs;
using lagalt_api.Models.DTOs.ProjectUsersDTOs;
using lagalt_api.Models.Domain;
using System.Linq;

namespace lagalt_api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UsersController : ControllerBase
    {
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(LagaltDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        /// <summary>
        /// Gets all users 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllUsers()
        {
            return _mapper.Map<List<UserReadDTO>>(await _context.Users
                .Include(u => u.Skills)
                .Include(u => u.Fields)
                .ToListAsync());

        }

        /// <summary>
        /// Get a specific user by id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUserById(string id)
        {
            var user = await _context.Users
                .Include(u => u.Skills)
                .Include(u => u.Fields)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserReadDTO>(user);

        }

        /// <summary>
        /// Gets all projects from a specific user
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        [HttpGet("/{id}/projects")]
        public async Task<ActionResult<ProjectUsersReadDTO>> GetProjectsByUserId(string id)
        {
            User UserObj = await _context.Users.Include(u => u.ContributedProjects).Where(f => f.UserId == id)
                .FirstAsync();

            if (UserObj == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProjectUsersReadDTO>(UserObj);
        }
    }
}
