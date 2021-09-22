using lagalt_api.Models.Domain;
using System.Collections.Generic;

namespace lagalt_api.Models.DTOs.ProjectUsersDTOs
{
    public class ProjectUsersReadDTO
    {
        public ICollection<Project> Projects { get; set; }
    }
}
