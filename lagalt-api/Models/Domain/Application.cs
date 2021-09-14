using System;
using System.ComponentModel.DataAnnotations;

namespace lagalt_api.Models.Domain
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public string UserId { get; set; }
        public int ProjectId { get; set; }

        [MaxLength(4000)]
        public string Motivation { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Approved { get; set; }
        public int ApprovedByOwnerId { get; set; }
    }
}
