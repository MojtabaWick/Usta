using Usta.Domain.Core.OfferAgg.Contracts;
using Usta.Domain.Core.OfferAgg.Dtos;

namespace Usta.Domain.AppService.OfferAgg
{
    public class OfferAppService(IOfferService offerService) : IOfferAppService
    {
        public async Task<List<OfferDto>> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await offerService.GetByOrderId(orderId, cancellationToken);
        }
    }
}