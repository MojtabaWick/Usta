using Usta.Domain.Core._common;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Dtos;

namespace Usta.Domain.Service.OrderAgg
{
    public class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        public async Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            return await orderRepository.GetAllOrders(pageNumber, pageSize, search, cancellationToken);
        }
    }
}