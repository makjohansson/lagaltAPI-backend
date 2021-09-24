using System;

namespace lagalt_api.Models.DTOs.ProjectDTOs
{
    public class ProjectCreateDTO
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string UrlReference { get; set; }
        public DateTime Created { get; set; }
    }
}

