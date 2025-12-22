using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;

namespace Usta.Domain.Core.CategoryAgg.Entities
{
    public class Category : BaseEntity
    {
        #region Properties

        public string Title { get; set; }
        public string? Description { get; set; }
        public string ImagedUrl { get; set; }

        #endregion Properties

        #region NavigationProperties

        public List<ProvidedService> ProvidedServices { get; set; } = [];

        #endregion NavigationProperties
    }
}