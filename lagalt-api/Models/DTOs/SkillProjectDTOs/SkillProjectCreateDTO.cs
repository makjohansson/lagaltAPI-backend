using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.SkillProjectDTOs
{
    public class SkillProjectCreateDTO
    {
        public int ProjectId { get; set; }
        public int SkillId { get; set; }
    }
}
