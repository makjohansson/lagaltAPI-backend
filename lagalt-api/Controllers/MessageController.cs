using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.MessagesDTOs;
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
    public class MessageController : ControllerBase
    {
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;

        public MessageController(LagaltDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Message>> AddMessage(MessageCreateDTO messageDto)
        {
            Message message = _mapper.Map<Message>(messageDto);
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = message.MessageId }, _mapper.Map<MessageCreateDTO>(message));
        }

        private bool Exists(int id)
        {
            return _context.Messages.Any(m => m.MessageId == id);
        }
    }
}
