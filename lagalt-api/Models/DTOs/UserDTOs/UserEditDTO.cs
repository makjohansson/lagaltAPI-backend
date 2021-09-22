
namespace lagalt_api.Models.DTOs.UserDTOs
{
    public class UserEditDTO
    {
        public string UserName { get; set; }
        public bool Hidden { get; set; }
        public string ProfilePhoto { get; set; }
        public string Description { get; set; }
    }
}
