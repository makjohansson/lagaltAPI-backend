using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lagalt_api.Models.Domain
{
    public class User
    {
        [Key]
        [StringLength(36)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        public ICollection<Field> Fields { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public bool Hidden { get; set; }
        
        [MaxLength(2083)]
        public string ProfilePhoto { get; set; }
        public ICollection<Project> SeenProjects { get; set; }
        public ICollection<Project> AppliedProjects { get; set; }
        public ICollection<Project> ClickedProjects { get; set; }
        public ICollection<Project> ContributedProjects { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
        public ICollection<Portfolio> Portfolios { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}