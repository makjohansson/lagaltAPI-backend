using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.PortfolioDTOs;

namespace lagalt_api.Profiles
{
    public class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {
            CreateMap<Portfolio, PortfolioCreateDTO>()
                .ReverseMap();
            CreateMap<Portfolio, PortfolioReadDTO>()
                .ReverseMap();
        }
    }
}