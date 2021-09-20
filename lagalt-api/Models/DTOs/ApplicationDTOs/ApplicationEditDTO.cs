using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.ApplicationDTOs
{
    public class ApplicationEditDTO
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public bool Approved { get; set; }
        public string ApprovedByOwnerId { get; set; }
    }
}
