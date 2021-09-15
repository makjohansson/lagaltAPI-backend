using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.ProjectUsersDTOs
{
    public class ProjectUsersCreateDTO
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public bool Owner { get; set; }

    }
}
