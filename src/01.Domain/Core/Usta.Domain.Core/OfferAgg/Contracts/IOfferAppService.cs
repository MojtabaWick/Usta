using Usta.Domain.Core._common;
using Usta.Domain.Core.OfferAgg.Dtos;

namespace Usta.Domain.Core.OfferAgg.Contracts
{
    public interface IOfferAppService
    {
        public Task<List<OfferDto>> GetByOrderId(int orderId, CancellationToken cancellationToken);

        public Task<Result<bool>> CreateOffer(CreateOfferDto input, CancellationToken cancellationToken);

        public Task<PagedResult<OfferDto>> GetExpertOffers(int expertId, int pageNumber, int pageSize, string? search, CancellationToken cancellationToken);
    }
}