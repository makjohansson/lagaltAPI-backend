using lagalt_api.Models.Domain.Enums;
using System.Collections.Generic;

namespace lagalt_api.Models.Domain
{
    public class Field
    {
        public int FieldId { get; set; }
        public FieldType FieldType { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
