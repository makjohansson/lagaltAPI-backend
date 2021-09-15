using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.PhotoDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Photo, PhotoDTO>()
                .ReverseMap();
        }
        
    }
}
