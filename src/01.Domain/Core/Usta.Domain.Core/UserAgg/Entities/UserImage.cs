using Usta.Domain.Core._common;

namespace Usta.Domain.Core.UserAgg.Entities
{
    public class UserImage : BaseEntity
    {
        public string ImageUrl { get; set; }

        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
    }
}