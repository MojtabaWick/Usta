using Usta.Domain.Core._common;
using Usta.Domain.Core.OfferAgg.Enums;
using Usta.Domain.Core.OrderAgg.Entities;
using Usta.Domain.Core.UserAgg.Entities;

namespace Usta.Domain.Core.OfferAgg.Entities
{
    public class Offer : BaseEntity
    {
        #region Properties

        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime StartDateTime { get; set; }
        public bool IsAccepted { get; set; }

        public OfferStatus Status { get; set; } = OfferStatus.WaitingForAccept;

        #endregion Properties

        #region NavigationProperties

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ExpertId { get; set; }
        public Expert Expert { get; set; }

        #endregion NavigationProperties
    }
}