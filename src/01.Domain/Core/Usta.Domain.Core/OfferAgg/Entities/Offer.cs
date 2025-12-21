using Usta.Domain.Core._common;
using Usta.Domain.Core.OrderAgg.Entities;
using Usta.Domain.Core.UserAgg.Entities;

namespace Usta.Domain.Core.OfferAgg.Entities
{
    public class Offer : BaseEntity
    {
        #region Properties

        public decimal Price { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public bool IsAccepted { get; set; }

        #endregion Properties

        #region NavigationProperties

        public List<OfferImage> Images { get; set; } = [];

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ExpertId { get; set; }
        public Expert Expert { get; set; }

        #endregion NavigationProperties
    }
}