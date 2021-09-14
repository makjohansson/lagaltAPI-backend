using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.UserDTOs;
using lagalt_api.Models.DTOs.ProjectUsersDTOs;
using System.Linq;

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
                .ReverseMap();

            CreateMap<User, ProjectUsersReadDTO>()
                .ForMember(pudto => pudto.ContributedProjects, opt => opt
                    .MapFrom(u => u.ContributedProjects.Select(p => p.ProjectName).ToList()))
                .ReverseMap();

            CreateMap<User, UserCreateDTO>()
                .ReverseMap();
        }
    }
}
