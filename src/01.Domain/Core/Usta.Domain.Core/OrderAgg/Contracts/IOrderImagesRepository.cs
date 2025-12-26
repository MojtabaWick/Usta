using Usta.Domain.Core.OrderAgg.Entities;

namespace Usta.Domain.Core.OrderAgg.Contracts
{
    public interface IOrderImagesRepository
    {
        public Task<bool> AddOrderImages(int orderId, List<OrderImage> newImages, CancellationToken cancellationToken);

        public Task<bool> DeleteOrderImages(int orderId, CancellationToken cancellationToken);
    }
}