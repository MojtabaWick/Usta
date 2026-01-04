using Usta.Domain.Core.OfferAgg.Contracts;
using Usta.Domain.Core.OfferAgg.Dtos;

namespace Usta.Domain.Service.OfferAgg
{
    public class OfferService(IOfferRepository offerRepository) : IOfferService
    {
        public async Task<List<OfferDto>> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await offerRepository.GetByOrderId(orderId, cancellationToken);
        }
    }
}