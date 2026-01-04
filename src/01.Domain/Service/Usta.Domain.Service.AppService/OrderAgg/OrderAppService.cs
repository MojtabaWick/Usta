using Usta.Domain.Core._common;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Dtos;

namespace Usta.Domain.AppService.OrderAgg
{
    public class OrderAppService(IOrderService orderService) : IOrderAppService
    {
        public async Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            return await orderService.GetAllOrders(pageNumber, pageSize, search, cancellationToken);
        }
    }
}