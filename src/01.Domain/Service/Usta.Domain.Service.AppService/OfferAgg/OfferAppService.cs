using Usta.Domain.Core._common;
using Usta.Domain.Core.OfferAgg.Contracts;
using Usta.Domain.Core.OfferAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Enums;

namespace Usta.Domain.AppService.OfferAgg
{
    public class OfferAppService(IOfferService offerService, IOrderService orderService) : IOfferAppService
    {
        public async Task<List<OfferDto>> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await offerService.GetByOrderId(orderId, cancellationToken);
        }

        public async Task<Result<bool>> CreateOffer(CreateOfferDto input, CancellationToken cancellationToken)
        {
            var orderStatus = await orderService.GetOrderStatus(input.OrderId, cancellationToken);
            if (orderStatus == OrderStatus.WaitingForOffers || orderStatus == OrderStatus.WaitingForAcceptance)
            {
                var result = await offerService.CreateOffer(input, cancellationToken);
                if (!result) return Result<bool>.Failure("ارسال پیشنهاد با خطا مواجه شده است.");

                if (orderStatus == OrderStatus.WaitingForOffers)
                {
                    await orderService.SetWaitingForAcceptance(input.OrderId, cancellationToken);
                }
                return Result<bool>.Success("پشنهاد با موفقیت ایجاد شد.");
            }

            return Result<bool>.Failure("نمیتوان پیشنهاد جدید ثبت کرد.");
        }

        public async Task<PagedResult<OfferDto>> GetExpertOffers(int expertId, int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            return await offerService.GetExpertOffers(expertId, pageNumber, pageSize, search, cancellationToken);
        }
    }
}