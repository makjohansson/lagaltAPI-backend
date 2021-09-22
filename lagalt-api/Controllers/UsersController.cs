
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using System.Linq;
using lagalt_api.Models.DTOs.UserDTOs;
using lagalt_api.Models.DTOs.FieldUserDTOs;
using lagalt_api.Models.DTOs.SkillUserDTOs;
using lagalt_api.Models.DTOs.ProjectDTOs;
using lagalt_api.Models.DTOs.ApplicationDTOs;

namespace lagalt_api.Controllers
{
    [Route("api/users")]
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
                .Include(u => u.Portfolios)
                .ToListAsync());
        }

        /// <summary>
        /// Get a specific user by id
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUserById(string id)
        {
            var user = await _context.Users
                .Include(u => u.Skills)
                .Include(u => u.Fields)
                .Include(u => u.Portfolios)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserReadDTO>(user);
        }

        /// <summary>
        /// Get all projects from a specific user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        [HttpGet("projects/{id}")]
        public async Task<ActionResult<IEnumerable<ProjectReadDTO>>> GetProjectsByUserId(string id)
        {
            var projectIdList = await _context.ProjectUsers
                .Where(pu => pu.UserId == id)
                .Select(pu => pu.ProjectId).ToListAsync();

            List<ProjectReadDTO> projectList = new List<ProjectReadDTO>();

            foreach (int projectId in projectIdList)
            {
                var project = _mapper.Map<ProjectReadDTO>(await _context.Projects
                .Include(p => p.Skills)
                .Include(p => p.Fields)
                .Include(p => p.ProjectUsers)
                .Include(p => p.Keywords)
                .Include(p => p.Photos)
                .Include(p => p.Messages)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId));

                projectList.Add(project);
            }

            return projectList;
        }

        /// <summary>
        /// Get all applications from a specific user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        [HttpGet("{userId}/applications")]
        public async Task<ActionResult<IEnumerable<ApplicationReadDTO>>> GetApplicationsByUserId(string userId)
        {
            return _mapper.Map<List<ApplicationReadDTO>>(await _context.Users
               .Where(p => p.UserId == userId)
                .SelectMany(p => p.Applications)
                .ToListAsync());
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="userDto">data for the new user</param>
        /// <returns>the created user</returns>
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(UserCreateDTO userDto)
        {
            User user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserById", new { id = user.UserId }, _mapper.Map<UserCreateDTO>(user));
        }

        /// <summary>
        /// Edit a specific user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="userDTO">data that should be added for the user</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> EditUser(string userId, UserEditDTO userDTO)
        {
            User user = _context.Users.Find(userId);
            if(user == null)
            {
                return NotFound($"No user with id {userId} was found");
            }

            user.UserName = userDTO.UserName;
            user.Hidden = userDTO.Hidden;
            user.ProfilePhoto = userDTO.ProfilePhoto;
            user.Description = userDTO.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add a specific field to specific user
        /// </summary>
        /// <param name="fieldUserIds">field id and user id</param>
        /// <returns></returns>
        [HttpPut("{userId}/field/{fieldId}")]
        public async Task<ActionResult> AddFieldToUser(FieldUserCreateDTO fieldUserIds)
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

        /// <summary>
        /// Add a specific skill to specific user
        /// </summary>
        /// <param name="skillUserIds"></param>
        /// <returns></returns>
        [HttpPut("{userId}/skill/{skillId}")]
        public async Task<ActionResult> AddSkillToUser(SkillUserCreateDTO skillUserIds)
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

        /// <summary>
        /// Delete a specific user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _context.Users.FindAsync(id);

            if (!Exists(id))
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Check if a specific user exists
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        private bool Exists(string id)
        {
            return _context.Users.Any(u => u.UserId == id);
        }
    }
}
