using lagalt_api.Models.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lagalt_api.Models.Domain
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProjectName { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public ICollection<Field> Fields { get; set; }
        public ICollection<Keyword> Keywords { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Application> Applications { get; set; }
        public ICollection<User> SeenByUsers { get; set; }
        public ICollection<User> AppliedByUsers { get; set; }
        public ICollection<User> ClickedByUsers { get; set; }
        public ICollection<User> ContributedByUsers { get; set; }
        public ProgressStatus Progress { get; set; }
        public DateTime Created { get; set; }
        public DateTime Closed { get; set; }

    }
}
