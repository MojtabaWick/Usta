using Usta.Domain.Core.OfferAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Entities;
using Usta.Domain.Core.OrderAgg.Enums;

namespace Usta.Domain.Core._common
{
    public class OrderAndOfferDto
    {
        public int Id { get; set; }
        public string ProvidedServiceTitle { get; set; }
        public string CustomerFullName { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string? StartShamsiDate { get; set; }
        public string? EndShamsiDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.WaitingForOffers;
        public int OffersCount { get; set; }
        public List<OrderImage> Images { get; set; } = [];
        public List<OfferDto> Offers { get; set; } = [];
    }
}