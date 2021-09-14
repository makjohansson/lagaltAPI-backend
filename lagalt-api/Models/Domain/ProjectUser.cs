using System.ComponentModel.DataAnnotations;

namespace lagalt_api.Models.Domain
{
    public class ProjectUser
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public bool Owner { get; set; }
    }
}
