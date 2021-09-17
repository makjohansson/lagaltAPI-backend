using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.ApplicationDTOs
{
    public class ApplicationCreateDTO
    {
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public string Motivation { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Approved { get; set; }
        public int ApprovedByOwnerId { get; set; }
    }
}
