using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.SkillDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
