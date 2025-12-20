using Usta.Domain.Core._common;
using Usta.Domain.Core.CategoryAgg.Entities;
using Usta.Domain.Core.OrderAgg.Entities;
using Usta.Domain.Core.UserAgg.Entities;

namespace Usta.Domain.Core.ProvidedServiceAgg.Entities
{
    public class ProvidedService : BaseEntity
    {
        #region Properties

        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }

        #endregion Properties

        #region NavigationProperties

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Order> Orders { get; set; } = [];
        public List<Expert> Experts { get; set; } = [];

        #endregion NavigationProperties
    }
}