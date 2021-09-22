using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.SkillDTOs;

namespace lagalt_api.Profiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<Skill, SkillReadDTO>()
                .ReverseMap();
            CreateMap<Skill, SkillCreateDTO>()
                .ReverseMap();
        }
    }
}