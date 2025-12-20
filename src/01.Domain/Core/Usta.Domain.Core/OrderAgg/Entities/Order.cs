using Usta.Domain.Core._common;
using Usta.Domain.Core.CommentAgg.Entities;
using Usta.Domain.Core.OfferAgg.Entities;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;
using Usta.Domain.Core.UserAgg.Entities;

namespace Usta.Domain.Core.OrderAgg.Entities
{
    public class Order : BaseEntity
    {
        #region Properties

        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public List<string> ImgUrList { get; set; }

        #endregion Properties

        #region NavigationProperties

        public ProvidedService ProvidedService { get; set; }
        public int ProvidedServiceId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int CommentId { get; set; }
        public Comment? Comment { get; set; }

        public List<Offer> Offers { get; set; } = [];

        #endregion NavigationProperties
    }
}