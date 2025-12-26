using Usta.Domain.Core.OrderAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Entities;

namespace Usta.Domain.Core.OrderAgg.Contracts
{
    public interface IOrderRepository
    {
        public Task<bool> Add(Order newOrder, CancellationToken cancellationToken);

        public Task<bool> Update(OrderEditInput input, CancellationToken cancellationToken);

        public Task<OrderDto?> GetById(int id, CancellationToken cancellationToken);

        public Task<List<OrderDto>> GetAllOrders(CancellationToken cancellationToken);
    }
}