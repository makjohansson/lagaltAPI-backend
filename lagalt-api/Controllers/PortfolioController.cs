using AutoMapper;
using lagalt_api.Data;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.PortfolioDTOs;
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
    public class PortfolioController : ControllerBase
    {
        private readonly LagaltDbContext _context;
        private readonly IMapper _mapper;

        public PortfolioController(LagaltDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PortfolioReadDTO>> GetById(int id)
        {
            if (!Exists(id))
            {
                return NotFound($"The portfolio with the id {id} was not found");
            }
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(p => p.PortfolioId == id);

            return _mapper.Map<PortfolioReadDTO>(portfolio);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Portfolio>> AddPortfolio(PortfolioCreateDTO portfolioDto)
        {
            Portfolio portfolio = _mapper.Map<Portfolio>(portfolioDto);
            _context.Portfolios.Add(portfolio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = portfolio.PortfolioId }, _mapper.Map<PortfolioCreateDTO>(portfolio));
        }

        private bool Exists(int id)
        {
            return _context.Portfolios.Any(p => p.PortfolioId == id);
        }
    }
}
