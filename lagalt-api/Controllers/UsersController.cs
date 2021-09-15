
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
using lagalt_api.Models.DTOs.UserDTOs;
using lagalt_api.Models.DTOs.FieldUserDTOs;
using lagalt_api.Models.DTOs.SkillUserDTOs;

namespace lagalt_api.Controllers
{
    [Route("users")]
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
        [HttpGet("projects/{id}")]
        public async Task<ActionResult<ProjectUsersReadDTO>> GetProjectsByUserId(string id)
        {
            User UserObj = await _context.Users
                .Include(u => u.ContributedProjects)
                .Where(u => u.UserId == id)
                .FirstAsync();

            if (UserObj == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProjectUsersReadDTO>(UserObj);
        }

        /// <summary>
        /// Add user to the databas
        /// </summary>
        /// <param name="userDto">data for the user</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(UserCreateDTO userDto)
        {
            User user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserById", new { id = user.UserId }, _mapper.Map<UserCreateDTO>(user));
        }

        [HttpPut("{userId}/field/{fieldId}")]
        public async Task<ActionResult> AddFieldToUser(string userId, int fieldId, FieldUserCreateDTO fieldUserIds)
        {
            User user = _context.Users.Include("Fields").First(u => u.UserId == fieldUserIds.UserId);
            user.Fields.Add(_context.Fields.Find(fieldUserIds.FieldId));
            try
            {
                await _context.SaveChangesAsync();
            }

            catch
            {
                throw;
            }

            return CreatedAtAction("AddFieldToUser", fieldUserIds);
        }


        [HttpPut("{userId}/skill/{skillId}")]
        public async Task<ActionResult> AddSkillToUser(string userId, int skillId, SkillUserCreateDTO skillUserIds)
        {
            User user = _context.Users.Include("Skills").First(u => u.UserId == skillUserIds.UserId);
            user.Skills.Add(_context.Skills.Find(skillUserIds.SkillId));
            try
            {
                await _context.SaveChangesAsync();
            }

            catch
            {
                throw;
            }

            return CreatedAtAction("AddSkillToUser", skillUserIds);
        }
    }
}
