using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.MessagesDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace lagalt_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class MessageController : ControllerBase
    {
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;

        public MessageController(LagaltDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a specific message by id
        /// </summary>
        /// <param name="id">message id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MessageReadDTO>> GetById(int id)
        {
            if (!Exists(id))
            {
                return NotFound($"The message with the id {id} was not found");
            }
            var message = await _context.Messages.FirstOrDefaultAsync(m => m.MessageId == id);

            return _mapper.Map<MessageReadDTO>(message);
        }

        /// <summary>
        /// Create a message
        /// </summary>
        /// <param name="messageDto">data for the new message</param>
        /// <returns>the created message</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Message>> Add(MessageCreateDTO messageDto)
        {
            Message message = _mapper.Map<Message>(messageDto);
            message.TimeStamp = DateTime.UtcNow;
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = message.MessageId }, _mapper.Map<MessageCreateDTO>(message));
        }

        /// <summary>
        /// Check if a specific message exists
        /// </summary>
        /// <param name="id">message id</param>
        /// <returns></returns>
        private bool Exists(int id)
        {
            return _context.Messages.Any(m => m.MessageId == id);
        }
    }
}
