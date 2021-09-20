﻿using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.KeywordDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace lagalt_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class KeywordController : ControllerBase
    {
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;

        public KeywordController(LagaltDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<KeywordReadDTO>>> GetAll()
        {
            return _mapper.Map<List<KeywordReadDTO>>(await _context.Keywords
                .ToListAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<KeywordReadDTO>> GetById(int id)
        {
            if (!Exists(id))
            {
                return NotFound($"The keyword with the id {id} was not found");
            }
            var keyword = await _context.Keywords.FirstOrDefaultAsync(k => k.KeywordId == id);

            return _mapper.Map<KeywordReadDTO>(keyword);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Keyword>> AddField(KeywordCreateDTO keywordDto)
        {
            Keyword keyword = _mapper.Map<Keyword>(keywordDto);
            _context.Keywords.Add(keyword);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = keyword.KeywordId }, _mapper.Map<KeywordCreateDTO>(keyword));
        }

        private bool Exists(int id)
        {
            return _context.Keywords.Any(k => k.KeywordId == id);
        }

    }
}
