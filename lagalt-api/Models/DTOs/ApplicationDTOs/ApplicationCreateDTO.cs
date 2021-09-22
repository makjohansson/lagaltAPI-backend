using System;

namespace lagalt_api.Models.DTOs.ApplicationDTOs
{
    public class ApplicationCreateDTO
    {
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public string Motivation { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
