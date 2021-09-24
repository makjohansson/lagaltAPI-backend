using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.PhotoDTOs;

namespace lagalt_api.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Photo, PhotoReadDTO>()
                .ReverseMap();
            CreateMap<Photo, PhotoCreateDTO>()
                .ReverseMap();
        }
    }
}
