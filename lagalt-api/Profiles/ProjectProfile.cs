using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.FieldProjectDTOs;
using lagalt_api.Models.DTOs.KeywordProjectCreateDTO;
using lagalt_api.Models.DTOs.ProjectDTOs;
using lagalt_api.Models.DTOs.ProjectUsersDTOs;
using System.Linq;

namespace lagalt_api.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectReadDTO>()
                .ForMember(pdto => pdto.Skills, opt => opt
                    .MapFrom(u => u.Skills.Select(s => s.SkillName).ToList()))
                .ForMember(pdto => pdto.Fields, opt => opt
                    .MapFrom(u => u.Fields.Select(f => f.FieldName).ToList()))
                .ForMember(pdto => pdto.Photos, opt => opt
                    .MapFrom(u => u.Photos.Select(p => p.PhotoUrl).ToList()))
                .ForMember(pdto => pdto.Keywords, opt => opt
                    .MapFrom(u => u.Keywords.Select(k => k.Tag).ToList()))
                .ForMember(pdto => pdto.ProjectUsers, opt => opt
                .MapFrom(u => u.ProjectUsers.Select(u => new { u.UserId, u.Owner }).ToList()))
                .ReverseMap();

            CreateMap<Project, KeywordProjectCreateDTO>()
                .ReverseMap();

            CreateMap<Project, ProjectCreateDTO>()
                .ReverseMap();

            CreateMap<Project, ProjectUsersCreateDTO>()
                .ReverseMap();

            CreateMap<Project, FieldProjectCreateDTO>()
                .ReverseMap();
        }
    }
}
