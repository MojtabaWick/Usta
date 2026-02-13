using Usta.Domain.Core._common;
using Usta.Domain.Core.OfferAgg.Dtos;
using Usta.Domain.Core.OfferAgg.Entities;
using Usta.Domain.Core.OrderAgg.Dtos;

namespace Usta.Domain.Core.OfferAgg.Contracts
{
    public interface IOfferRepository
    {
        public Task<bool> CreateOffer(Offer newOffer, CancellationToken cancellationToken);

        public Task<List<OfferDto>> GetByOrderId(int orderId, CancellationToken cancellationToken);

        public Task<bool> CheckOfferExist(int offerId, CancellationToken cancellationToken);

        public Task<bool> AcceptOffer(int offerId, CancellationToken cancellationToken);

        public Task<PagedResult<OfferDto>> GetExpertOffers(int expertId, int pageNumber, int pageSize, string? search, CancellationToken cancellationToken);
    }
}