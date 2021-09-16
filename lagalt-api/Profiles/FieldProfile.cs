using AutoMapper;
using lagalt_api.Models.Domain;
using System.Linq;

using lagalt_api.Models.DTOs.FieldDTOs;

namespace lagalt_api.Profiles
{
    public class FieldProfile : Profile
    {
        public FieldProfile()
        {
            CreateMap<Field, FieldCreateDTO>()
                .ReverseMap();
            CreateMap<Field, FieldReadDTO>()
                .ReverseMap();
        }
    }
}