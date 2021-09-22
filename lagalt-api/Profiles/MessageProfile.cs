using AutoMapper;
using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.MessagesDTOs;

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
