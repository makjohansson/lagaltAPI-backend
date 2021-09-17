using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lagalt_api.Models.DTOs.PortfolioDTOs
{
    public class PortfolioReadDTO
    {
        public int PortfolioId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public DateTime TimeSpanStart { get; set; }
        public DateTime TimeSpanEnd { get; set; }
        public string UrlReference { get; set; }
        public string Description { get; set; }
    }
}
