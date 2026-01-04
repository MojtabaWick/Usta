using System;
using System.Collections.Generic;
using System.Text;

namespace Usta.Domain.Core.OfferAgg.Dtos
{
    public class OfferDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime StartDateTime { get; set; }
        public bool IsAccepted { get; set; }
        public int ExpertId { get; set; }
        public string ExpertName { get; set; }
    }
}