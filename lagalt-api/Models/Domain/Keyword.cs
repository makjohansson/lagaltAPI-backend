using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lagalt_api.Models.Domain
{
    public class Keyword
    {
        public int KeywordId { get; set; }
        [Required]
        [MaxLength(70)]
        public string Tag { get; set;  }
        public ICollection<Project> Projects { get; set; }

    }
}
