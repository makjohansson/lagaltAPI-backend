using lagalt_api.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.ProjectDTOs
{
    public class ProjectParamReadDTO
    {
        public int Count { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ICollection<ProjectReadDTO> Projects { get; set; }
    }
}
