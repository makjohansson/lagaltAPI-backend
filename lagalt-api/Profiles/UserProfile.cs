using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.UserDTOs;
using lagalt_api.Models.DTOs.ProjectUsersDTOs;
using System.Linq;
using lagalt_api.Models.DTOs.SkillUserDTOs;
using lagalt_api.Models.DTOs.FieldUserDTOs;
using System.Collections.Generic;

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
            /*
            CreateMap<User, ProjectUsersReadDTO>()
                .ForMember(pudto => pudto.ContributedProjects, opt => opt
                    .MapFrom(u => u.ContributedProjects.Select(p => p.ProjectName).ToList()))
                .ReverseMap();
            
            
            CreateMap<IEnumerable<ProjectUser>, ProjectUsersReadDTO>()
                .ForMember(pudto => pudto.Projects, opt => opt
                    .MapFrom(p => p.Select(p => p.Projects)
                    .ToList()))
                .ReverseMap();
            
            */
            CreateMap<User, UserCreateDTO>()
                .ReverseMap();

            CreateMap<User, SkillUserCreateDTO>()
                .ReverseMap();

            CreateMap<User, FieldUserCreateDTO>()
                .ReverseMap();
        }
    }
}
