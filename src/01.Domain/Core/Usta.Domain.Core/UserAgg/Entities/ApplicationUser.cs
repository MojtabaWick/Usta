using Microsoft.AspNetCore.Identity;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CityAgg.Entities;
using Usta.Domain.Core.CommentAgg.Entities;

namespace Usta.Domain.Core.UserAgg.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        #region Properties

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public decimal WalletBalance { get; set; } = 0;

        #endregion Properties

        #region NavigationProperties

        public List<Comment> Comments { get; set; } = [];
        public int CityId { get; set; }
        public City City { get; set; }

        #endregion NavigationProperties
    }
}