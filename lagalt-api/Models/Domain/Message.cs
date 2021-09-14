using System;
using System.ComponentModel.DataAnnotations;

namespace lagalt_api.Models.Domain
{
    public class Message
    {
        public int MessageId { get; set; }
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        
        [Required]
        [MaxLength(4000)]
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
