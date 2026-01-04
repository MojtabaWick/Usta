using Usta.Domain.Core.OfferAgg.Dtos;

namespace Usta.Domain.Core.OfferAgg.Contracts
{
    public interface IOfferAppService
    {
        public Task<List<OfferDto>> GetByOrderId(int orderId, CancellationToken cancellationToken);
    }
}