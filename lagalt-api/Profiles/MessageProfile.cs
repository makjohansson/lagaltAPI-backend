﻿using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.MessagesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageCreateDTO>()
                .ReverseMap();
            CreateMap<Message, MessageReadDTO>()
                .ReverseMap();
        }
 
    }
}
