using lagalt_api.Models.Domain.Enums;

namespace lagalt_api.Models.DTOs.ProjectDTOs
{
    public class ProjectEditDTO
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string UrlReference { get; set; }
        public ProgressStatus Progress { get; set; }
    }
}
