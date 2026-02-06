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

        public async Task<bool> CheckOfferExist(int offerId, CancellationToken cancellationToken)
        {
            return await offerRepository.CheckOfferExist(offerId, cancellationToken);
        }

        public async Task<bool> AcceptOffer(int offerId, CancellationToken cancellationToken)
        {
            return await offerRepository.AcceptOffer(offerId, cancellationToken);
        }
    }
}