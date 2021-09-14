using System.Collections.Generic;

namespace lagalt_api.Models.DTOs
{
    public class UserReadDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ProfilePhoto { get; set; }
        public string Description { get; set; }
        public bool Hidden { get; set; }
        public List<string> Skills { get; set; }
        public List<string> Fields { get; set; }
    }
}
