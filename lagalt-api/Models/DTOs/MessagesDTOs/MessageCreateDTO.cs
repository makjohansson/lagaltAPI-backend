using System;

namespace lagalt_api.Models.DTOs.MessagesDTOs
{
    public class MessageCreateDTO
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
