using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.ApplicationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        }
    }
}
