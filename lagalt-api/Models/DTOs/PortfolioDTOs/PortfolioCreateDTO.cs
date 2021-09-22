using System;

namespace lagalt_api.Models.DTOs.PortfolioDTOs
{
    public class PortfolioCreateDTO
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public DateTime TimeSpanStart { get; set; }
        public DateTime TimeSpanEnd { get; set; }
        public string UrlReference { get; set; }
        public string Description { get; set; }
    }
}
