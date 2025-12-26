using Usta.Domain.Core.OrderAgg.Entities;
using Usta.Domain.Core.OrderAgg.Enums;

namespace Usta.Domain.Core.OrderAgg.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.WaitingForOffers;
        public int OffersCount { get; set; }
        public List<OrderImage> Images { get; set; } = [];
    }
}