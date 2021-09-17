using lagalt_api.Models.Domain;
using lagalt_api.Models.DTOs.PortfolioDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.UserDTOs
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
        public ICollection<PortfolioReadDTO> Portfolios { get; set; }

    }
}
