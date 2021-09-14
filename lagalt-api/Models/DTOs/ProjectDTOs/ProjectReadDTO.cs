using lagalt_api.Models.Domain;
using lagalt_api.Models.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.ProjectDTOs
{
    public class ProjectReadDTO
    {
        public string ProjectName { get; set; }
        public ICollection<string> ProjectUsers { get; set; }
        public ICollection<string> Skills { get; set; }
        public ICollection<string> Fields { get; set; }
        public ICollection<string> Keywords { get; set; }
        public ICollection<string> Photos { get; set; }
        public ICollection<string> Messages { get; set; }
        public ProgressStatus Progress { get; set; }
        public DateTime Created { get; set; }
        public DateTime Closed { get; set; }
    }
}
