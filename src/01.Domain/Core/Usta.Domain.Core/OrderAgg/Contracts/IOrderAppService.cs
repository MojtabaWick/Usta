using System.Threading;
using Usta.Domain.Core._common;
using Usta.Domain.Core.OfferAgg.Entities;
using Usta.Domain.Core.OrderAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Entities;

namespace Usta.Domain.Core.OrderAgg.Contracts
{
    public interface IOrderAppService
    {
        Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken);

        Task<Result<bool>> CreateOrder(CreateOrderDto dto, int customerId, CancellationToken cancellationToken);

        Task<PagedResult<OrderAndOfferDto>> GetCustomerOrders(int customerId, int pageNumber, int pageSize,
            string? search, CancellationToken cancellationToken);

        Task<Result<bool>> AcceptOffer(int orderId, int offerId, CancellationToken cancellationToken);

        Task<Result<bool>> SetOrderDone(int orderId, CancellationToken cancellationToken);

        Task<Result<bool>> PayOrder(int orderId, CancellationToken cancellationToken);
    }
}