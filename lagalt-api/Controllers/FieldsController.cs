using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.Domain.Enums;
using lagalt_api.Models.DTOs.FieldDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace lagalt_api.Controllers
{
    [Route("fields")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FieldsController : ControllerBase
    {
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;

        public FieldsController(LagaltDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Field>> AddField (FieldCreateDTO fieldDto)
        {
            Field field = _mapper.Map<Field>(fieldDto);
            _context.Fields.Add(field);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllFields", new { id = field.FieldId }, _mapper.Map<FieldCreateDTO>(field));
        }
    }
}
