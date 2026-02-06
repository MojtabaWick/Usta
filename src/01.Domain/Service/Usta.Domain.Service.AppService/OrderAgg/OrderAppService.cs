using Microsoft.Extensions.Logging;
using Usta.Domain.Core._common;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Dtos;
using Usta.Domain.Core.UserAgg.Contracts;

namespace Usta.Domain.AppService.OrderAgg
{
    public class OrderAppService(IOrderService orderService, IUserService userService, ILogger<OrderAppService> _logger) : IOrderAppService
    {
        public async Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            return await orderService.GetAllOrders(pageNumber, pageSize, search, cancellationToken);
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
    }
}