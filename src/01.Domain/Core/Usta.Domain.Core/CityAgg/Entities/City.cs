using Usta.Domain.Core._common;
using Usta.Domain.Core.UserAgg.Entities;

namespace Usta.Domain.Core.CityAgg.Entities
{
    public class City : BaseEntity
    {
        #region Properties

        public string Name { get; set; }

        #endregion Properties

        #region NavigationProperties

        public List<ApplicationUser> Users { get; set; } = [];

        #endregion NavigationProperties
    }
}