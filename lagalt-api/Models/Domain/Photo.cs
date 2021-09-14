using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lagalt_api.Models.Domain
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(2083)]
        public string PhotoUrl { get; set; }
    }
}
