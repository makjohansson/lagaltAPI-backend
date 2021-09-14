using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lagalt_api.Models.Domain
{
    public class Skill
    {
        public int SkillId { get; set; }

        [Required]
        [MaxLength(70)]
        public string SkillName { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
