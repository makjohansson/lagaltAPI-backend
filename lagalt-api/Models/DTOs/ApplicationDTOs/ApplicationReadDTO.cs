using System;

namespace lagalt_api.Models.DTOs.ApplicationDTOs
{
    public class ApplicationReadDTO
    {
        public int ApplicationId { get; set; }
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public string Motivation { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Approved { get; set; }
        public string ApprovedByOwnerId { get; set; }
    }
}
