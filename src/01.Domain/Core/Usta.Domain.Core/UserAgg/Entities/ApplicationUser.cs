using Microsoft.AspNetCore.Identity;
using Usta.Domain.Core.CityAgg.Entities;

namespace Usta.Domain.Core.UserAgg.Entities
{
    public abstract class ApplicationUser : IdentityUser<int>
    {
        #region Properties

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public decimal WalletBalance { get; set; } = 0;

        #endregion Properties

        #region NavigationProperties

        public int? CityId { get; set; }
        public City? City { get; set; }

        #endregion NavigationProperties
    }
}