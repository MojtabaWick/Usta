using Usta.Domain.Core._common;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Entities;
using Usta.Domain.Core.OrderAgg.Enums;
using Usta.Framework;
using Usta.Infrastructure.FileService.Contracts;

namespace Usta.Domain.Service.OrderAgg
{
    public class OrderService(IOrderRepository orderRepository, IOrderImagesRepository orderImagesRepository, IFileService fileService) : IOrderService
    {
        public async Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            return await orderRepository.GetAllOrders(pageNumber, pageSize, search, cancellationToken);
        }

        public async Task<bool> CreateOrder(CreateOrderDto dto, int customerId, CancellationToken cancellationToken)
        {
            var startDate = dto.StartDate.ToGregorianDateTime();

            var order = new Order
            {
                Description = dto.Description,
                StartDateTime = startDate,
                ProvidedServiceId = dto.ProvidedServiceId,
                CustomerId = customerId,
                Status = OrderStatus.WaitingForOffers
            };

            var result = await orderRepository.Add(order, cancellationToken);

            if (!result)
                return false;

            if (dto.Images is not null && dto.Images.Any())
            {
                var imageUrls = await fileService.UploadMany(
                    dto.Images,
                    "Orders",
                    cancellationToken);

                var orderImages = imageUrls.Select(url => new OrderImage
                {
                    ImageUrl = url,
                    OrderId = order.Id
                }).ToList();

                await orderImagesRepository.AddOrderImages(order.Id, orderImages, cancellationToken);
            }

            return true;
        }

        public async Task<PagedResult<OrderAndOfferDto>> GetCustomerOrders(int customerId, int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken)
        {
            return await orderRepository.GetCustomerOrders(customerId, pageNumber, pageSize, search, cancellationToken);
        }

        public async Task<decimal> GetPriceByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await orderRepository.GetPriceByOrderId(orderId, cancellationToken);
        }

        public async Task<int> GetExpertIdByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await orderRepository.GetExpertIdByOrderId(orderId, cancellationToken);
        }

        public async Task<int> GetCustomerIdByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await orderRepository.GetCustomerIdByOrderId(orderId, cancellationToken);
        }

        public async Task<bool> SetWaitingForPayment(int orderId, CancellationToken cancellationToken)
        {
            return await orderRepository.SetWaitingForPayment(orderId, cancellationToken);
        }

        public async Task SetDone(int orderId, CancellationToken cancellationToken)
        {
            await orderRepository.SetDone(orderId, cancellationToken);
        }

        public async Task<bool> OrderAcceptOffer(int orderId, int offerId, CancellationToken cancellationToken)
        {
            return await orderRepository.OrderAcceptOffer(orderId, offerId, cancellationToken);
        }

        public async Task<bool> CheckOrderAcceptedOffer(int orderId, CancellationToken cancellationToken)
        {
            return await orderRepository.CheckOrderAcceptedOffer(orderId, cancellationToken);
        }

        public async Task<bool> checkOrderExist(int orderId, CancellationToken cancellationToken)
        {
            return await orderRepository.checkOrderExist(orderId, cancellationToken);
        }

        public async Task<bool> OrderIsCompleted(int orderId, CancellationToken cancellationToken)
        {
            return await orderRepository.OrderIsCompleted(orderId, cancellationToken);
        }

        public async Task<bool> OrderHasComment(int orderId, CancellationToken cancellationToken)
        {
            return await orderRepository.OrderHasComment(orderId, cancellationToken);
        }
    }
}