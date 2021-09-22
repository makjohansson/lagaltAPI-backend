using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.KeywordDTOs;

namespace lagalt_api.Profiles
{
    public class KeywordProfile : Profile
    {
        public KeywordProfile()
        {
            CreateMap<Keyword, KeywordReadDTO>()
                .ReverseMap();

            CreateMap<Keyword, KeywordCreateDTO>()
                    .ReverseMap();
        }
    }
}