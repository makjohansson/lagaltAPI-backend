using lagalt_api.Models.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lagalt_api.Models.Domain
{
    public class Field
    {
        public int FieldId { get; set; }
        [Required]
        [MaxLength(70)]
        public string FieldName { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
