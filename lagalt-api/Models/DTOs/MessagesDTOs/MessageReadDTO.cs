using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.MessagesDTOs
{
    public class MessageReadDTO
    {
        public int MessageId { get; set; }
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
