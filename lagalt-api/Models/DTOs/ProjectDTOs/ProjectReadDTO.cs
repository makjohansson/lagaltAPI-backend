using lagalt_api.Models.Domain.Enums;
using System;
using System.Collections.Generic;

namespace lagalt_api.Models.DTOs.ProjectDTOs
{
    public class ProjectReadDTO
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string UrlReference { get; set; }
        public ICollection<Object> ProjectUsers { get; set; }
        public ICollection<string> Skills { get; set; }
        public ICollection<string> Fields { get; set; }
        public ICollection<string> Keywords { get; set; }
        public ICollection<string> Photos { get; set; }
        public ProgressStatus Progress { get; set; }
        public DateTime Created { get; set; }
        public DateTime Closed { get; set; }
    }
}
