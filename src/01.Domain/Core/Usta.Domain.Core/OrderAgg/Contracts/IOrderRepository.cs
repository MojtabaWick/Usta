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

        Task<PagedResult<OrderDto>> GetOrdersForExpert(List<int> expertServices, int? cityId, int pageNumber, int pageSize, string? search, CancellationToken cancellationToken);

        Task<PagedResult<OrderAndOfferDto>> GetCustomerOrders(int customerId, int pageNumber, int pageSize,
            string? search, CancellationToken cancellationToken);

        Task<decimal> GetPriceByOrderId(int orderId, CancellationToken cancellationToken);

        Task<int> GetExpertIdByOrderId(int orderId, CancellationToken cancellationToken);

        Task<int> GetCustomerIdByOrderId(int orderId, CancellationToken cancellationToken);

        Task<bool> SetWaitingForPayment(int orderId, CancellationToken cancellationToken);

        Task<bool> OrderAcceptOffer(int orderId, int offerId, CancellationToken cancellationToken);

        Task<bool> CheckOrderAcceptedOffer(int orderId, CancellationToken cancellationToken);

        Task<bool> checkOrderExist(int orderId, CancellationToken cancellationToken);

        Task SetDone(int orderId, CancellationToken cancellationToken);

        Task<bool> OrderIsCompleted(int orderId, CancellationToken cancellationToken);

        Task<bool> OrderHasComment(int orderId, CancellationToken cancellationToken);
    }
}