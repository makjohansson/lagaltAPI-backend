using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.KeywordDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Profiles
{
    public class KeywordProfile : Profile
    {
        public KeywordProfile()
        {
            CreateMap<Keyword, KeywordDTO>()
                .ReverseMap();
        }
    }
}
