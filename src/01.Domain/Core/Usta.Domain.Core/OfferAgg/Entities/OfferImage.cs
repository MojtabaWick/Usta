using Usta.Domain.Core._common;
using Usta.Domain.Core.CategoryAgg.Entities;

namespace Usta.Domain.Core.OfferAgg.Entities
{
    public class OfferImage : BaseEntity
    {
        public string ImageUrl { get; set; }

        public Offer Offer { get; set; }
        public int OfferId { get; set; }
    }
}