
namespace lagalt_api.Models.DTOs.ProjectUsersDTOs
{
    public class ProjectUsersCreateDTO
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public bool Owner { get; set; }
    }
}
