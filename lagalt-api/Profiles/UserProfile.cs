using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.UserDTOs;
using System.Linq;
using lagalt_api.Models.DTOs.SkillUserDTOs;
using lagalt_api.Models.DTOs.FieldUserDTOs;

namespace lagalt_api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDTO>()
                .ForMember(udto => udto.Skills, opt => opt
                    .MapFrom(u => u.Skills.Select(s => s.SkillName).ToList()))
                .ForMember(udto => udto.Fields, opt => opt
                    .MapFrom(u => u.Fields.Select(f => f.FieldName).ToList()))
                .ForMember(udto => udto.Portfolios, opt => opt
                    .MapFrom(u => u.Portfolios.ToList()))
                .ReverseMap();

            CreateMap<User, UserCreateDTO>()
                .ReverseMap();

            CreateMap<User, SkillUserCreateDTO>()
                .ReverseMap();

            CreateMap<User, FieldUserCreateDTO>()
                .ReverseMap();
        }
    }
}
