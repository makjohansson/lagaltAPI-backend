using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.FieldUserDTOs
{
    public class FieldUserCreateDTO
    {
        public string UserId { get; set; }
        public int FieldId { get; set; }
    }
}
