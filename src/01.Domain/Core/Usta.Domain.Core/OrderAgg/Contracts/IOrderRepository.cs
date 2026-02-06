using Usta.Domain.Core._common;
using Usta.Domain.Core.OrderAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Entities;

namespace Usta.Domain.Core.OrderAgg.Contracts
{
    public interface IOrderRepository
    {
        Task<bool> Add(Order newOrder, CancellationToken cancellationToken);

        Task<bool> Update(OrderEditInput input, CancellationToken cancellationToken);

        Task<OrderDto?> GetById(int id, CancellationToken cancellationToken);

        Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken);

        Task<PagedResult<OrderAndOfferDto>> GetCustomerOrders(int customerId, int pageNumber, int pageSize,
            string? search, CancellationToken cancellationToken);
    }
}