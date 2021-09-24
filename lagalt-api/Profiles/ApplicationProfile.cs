using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.ApplicationDTOs;

namespace lagalt_api.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Application, ApplicationReadDTO>()
                .ReverseMap();
            CreateMap<Application, ApplicationCreateDTO>()
                .ReverseMap();
            CreateMap<Application, ApplicationEditDTO>()
                .ReverseMap();
        }
    }
}
