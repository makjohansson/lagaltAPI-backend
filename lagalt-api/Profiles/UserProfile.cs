using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs;
using lagalt_api.Models.DTOs.ProjectUsersDTOs;
using System.Linq;

namespace lagalt_api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDTO>()
                .ForMember(cdto => cdto.Skills, opt => opt
                    .MapFrom(c => c.Skills.Select(m => m.SkillName).ToList()))
                .ForMember(cdto => cdto.Fields, opt => opt
                    .MapFrom(c => c.Fields.Select(m => m.FieldType).ToList()))
                .ReverseMap();

            CreateMap<User, ProjectUsersReadDTO>()
                .ForMember(cdto => cdto.ContributedProjects, opt => opt
                    .MapFrom(c => c.ContributedProjects.Select(m => m.ProjectName).ToList()))
                .ReverseMap();
        }
    }
}
