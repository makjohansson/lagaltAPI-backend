using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

