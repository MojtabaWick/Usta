using Usta.Domain.Core._common;
using Usta.Domain.Core.OrderAgg.Dtos;

namespace Usta.Domain.Core.OrderAgg.Contracts
{
    public interface IOrderAppService
    {
        public Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken);

        public Task<Result<bool>> CreateOrder(CreateOrderDto dto, int customerId, CancellationToken cancellationToken);
    }
}