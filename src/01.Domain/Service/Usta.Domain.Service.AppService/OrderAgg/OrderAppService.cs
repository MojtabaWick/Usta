using System.Transactions;
using Microsoft.Extensions.Logging;
using Usta.Domain.Core._common;
using Usta.Domain.Core.OfferAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Dtos;
using Usta.Domain.Core.UserAgg.Contracts;

namespace Usta.Domain.AppService.OrderAgg
{
    public class OrderAppService(IOrderService orderService,
        IOfferService offerService, IUserService userService,
        ILogger<OrderAppService> _logger)
        : IOrderAppService
    {
        public async Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            return await orderService.GetAllOrders(pageNumber, pageSize, search, cancellationToken);
        }

        public async Task<PagedResult<OrderDto>> GetOrdersForExpert(int expertId, int? cityId, int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken)
        {
            var expertProvidedServicesIds = await userService.GetExpertProvidedServicesIds(expertId, cancellationToken);

            return await orderService.GetOrdersForExpert(expertProvidedServicesIds, cityId, pageNumber, pageSize, search, cancellationToken);
        }

        public async Task<Result<bool>> CreateOrder(CreateOrderDto dto, int customerId, CancellationToken cancellationToken)
        {
            var checkUserProfile = await userService.CheckUserProfile(customerId, cancellationToken);
            if (checkUserProfile)
            {
                var resultBool = await orderService.CreateOrder(dto, customerId, cancellationToken);
                if (resultBool)
                {
                    _logger.LogInformation("new order created.");
                    return Result<bool>.Success("ثبت سفارش با موفقیت انجام شد.");
                }
                else
                {
                    _logger.LogWarning("can't create a new order.");
                    return Result<bool>.Failure("ایجاد سفارش با مشکل مواجه شده است.");
                }
            }
            else
            {
                return Result<bool>.Failure("خطا:لطفا پروفایل خود را در پنل کاربری تکمیل کنید.");
            }
        }

        public async Task<PagedResult<OrderAndOfferDto>> GetCustomerOrders(int customerId, int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken)
        {
            return await orderService.GetCustomerOrders(customerId, pageNumber, pageSize, search, cancellationToken);
        }

        public async Task<Result<bool>> AcceptOffer(int orderId, int offerId, CancellationToken cancellationToken)
        {
            var OrderExist = await orderService.checkOrderExist(orderId, cancellationToken);
            if (!OrderExist)
                return Result<bool>.Failure("سفارش شما پیدا نشد.");

            var checkOrderAcceptedOffer = await orderService.CheckOrderAcceptedOffer(orderId, cancellationToken);
            if (checkOrderAcceptedOffer)
                return Result<bool>.Failure("در سفارش شما قبلا پیشنهادی قبول شده است، نمیتوان پیشنهاد جدید قبول کرد.");

            var OfferExist = await offerService.CheckOfferExist(offerId, cancellationToken);
            if (!OfferExist)
                return Result<bool>.Failure("پیشنهاد انتخابی پیدا نشد.");

            var acceptResult = await orderService.OrderAcceptOffer(orderId, offerId, cancellationToken);

            if (acceptResult)
            {
                var offerIsAccepted = await offerService.AcceptOffer(offerId, cancellationToken);
                if (offerIsAccepted)
                {
                    return Result<bool>.Success("پیشنهاد با موفقیت تایید شد.");
                }
            }

            return Result<bool>.Failure("خطا در قبول کردن پیشنهاد.");
        }

        public async Task<Result<bool>> SetOrderDone(int orderId, CancellationToken cancellationToken)
        {
            var result = await orderService.SetWaitingForPayment(orderId, cancellationToken);
            return result ? Result<bool>.Success("وضعیت سفارش به در انتظار پرداخت تغییر کرد.")
                : Result<bool>.Failure("تغییر وضعیت سفارش با خطا مواجه شده است.");
        }

        public async Task<Result<bool>> PayOrder(int orderId, CancellationToken cancellationToken)
        {
            var customerId = await orderService.GetCustomerIdByOrderId(orderId, cancellationToken);
            var expertId = await orderService.GetExpertIdByOrderId(orderId, cancellationToken);
            var price = await orderService.GetPriceByOrderId(orderId, cancellationToken);
            var checkWalletBalance = await userService.CheckUserWalletBalance(customerId, price, cancellationToken);
            if (!checkWalletBalance)
            {
                return Result<bool>.Failure("موجودی حساب شما کافی نمیباشد.");
            }

            using var transaction = new TransactionScope(
                TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                await userService.DecreaseWallet(customerId, price, cancellationToken);
                await userService.IncreaseWallet(expertId, price, cancellationToken);
                await orderService.SetDone(orderId, cancellationToken);

                transaction.Complete();

                return Result<bool>.Success("سفارش با موفقیت پرداخت شد.");
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning($"Order payment canceled. OrderId:{orderId}");
                return Result<bool>.Failure("پرداخت لغو شد.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Order payment error. OrderId:{orderId}");
                return Result<bool>.Failure("پرداخت سفارش با مشکل مواجه شده است.");
            }
        }
    }
}