using System.Threading;
using Usta.Domain.Core._common;
using Usta.Domain.Core.OfferAgg.Entities;
using Usta.Domain.Core.OrderAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Entities;

namespace Usta.Domain.Core.OrderAgg.Contracts
{
    public interface IOrderService
    {
        Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken);

        Task<bool> CreateOrder(CreateOrderDto dto, int customerId, CancellationToken cancellationToken);

        Task<PagedResult<OrderAndOfferDto>> GetCustomerOrders(int customerId, int pageNumber, int pageSize,
            string? search, CancellationToken cancellationToken);

        Task<decimal> GetPriceByOrderId(int orderId, CancellationToken cancellationToken);

        Task<int> GetExpertIdByOrderId(int orderId, CancellationToken cancellationToken);

        Task<int> GetCustomerIdByOrderId(int orderId, CancellationToken cancellationToken);

        Task<bool> SetWaitingForPayment(int orderId, CancellationToken cancellationToken);

        Task SetDone(int orderId, CancellationToken cancellationToken);

        Task<bool> OrderAcceptOffer(int orderId, int offerId, CancellationToken cancellationToken);

        Task<bool> CheckOrderAcceptedOffer(int orderId, CancellationToken cancellationToken);

        Task<bool> checkOrderExist(int orderId, CancellationToken cancellationToken);
    }
}