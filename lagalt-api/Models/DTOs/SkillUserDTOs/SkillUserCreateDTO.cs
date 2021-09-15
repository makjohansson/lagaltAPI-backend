using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.SkillUserDTOs
{
    public class SkillUserCreateDTO
    {
        public string UserId { get; set; }
        public int SkillId { get; set; }
    }
}
