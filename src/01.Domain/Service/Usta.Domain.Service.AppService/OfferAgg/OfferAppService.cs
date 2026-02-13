using Usta.Domain.Core._common;
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

        public async Task<Result<bool>> CreateOffer(CreateOfferDto input, CancellationToken cancellationToken)
        {
            var result = await offerService.CreateOffer(input, cancellationToken);
            return result ? Result<bool>.Success("پشنهاد با موفقیت ایجاد شد.")
                : Result<bool>.Failure("ارسال پیشنهاد با خطا مواجه شده است.");
        }
    }
}