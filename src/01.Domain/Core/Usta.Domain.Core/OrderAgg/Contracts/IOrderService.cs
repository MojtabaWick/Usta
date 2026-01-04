using Usta.Domain.Core._common;
using Usta.Domain.Core.OrderAgg.Dtos;

namespace Usta.Domain.Core.OrderAgg.Contracts
{
    public interface IOrderService
    {
        public Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken);
    }
}