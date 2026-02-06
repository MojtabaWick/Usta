using Usta.Domain.Core._common;
using Usta.Domain.Core.OrderAgg.Dtos;

namespace Usta.Domain.Core.OrderAgg.Contracts
{
    public interface IOrderService
    {
        Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken);

        Task<bool> CreateOrder(CreateOrderDto dto, int customerId, CancellationToken cancellationToken);

        Task<PagedResult<OrderAndOfferDto>> GetCustomerOrders(int customerId, int pageNumber, int pageSize,
            string? search, CancellationToken cancellationToken);
    }
}