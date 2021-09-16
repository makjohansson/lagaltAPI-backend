using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.PhotoDTOs
{
    public class PhotoCreateDTO
    {
        public int ProjectId { get; set; }
        public string PhotoUrl { get; set; }
    }
}
