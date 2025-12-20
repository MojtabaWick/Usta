using Usta.Domain.Core.CommentAgg.Entities;
using Usta.Domain.Core.OrderAgg.Entities;

namespace Usta.Domain.Core.UserAgg.Entities
{
    public class Customer : ApplicationUser
    {
        public List<Order> Orders { get; set; } = [];
    }
}