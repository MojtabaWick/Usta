using Usta.Domain.Core._common;
using Usta.Domain.Core.OrderAgg.Entities;
using Usta.Domain.Core.UserAgg.Entities;

namespace Usta.Domain.Core.CommentAgg.Entities
{
    public class Comment : BaseEntity
    {
        #region Properties

        public string Text { get; set; }
        public int Rating { get; set; }
        public bool IsApproved { get; set; } = false;

        #endregion Properties

        #region NavigationProperties

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ExpertId { get; set; }
        public Expert Expert { get; set; }

        #endregion NavigationProperties
    }
}