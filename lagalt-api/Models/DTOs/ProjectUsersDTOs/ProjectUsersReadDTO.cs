using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.ProjectDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.ProjectUsersDTOs
{
    public class ProjectUsersReadDTO
    {
        public ICollection<Project> ContributedProjects { get; set; }
    }
}
