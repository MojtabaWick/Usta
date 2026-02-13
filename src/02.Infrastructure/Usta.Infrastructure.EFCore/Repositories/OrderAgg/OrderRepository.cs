using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CommentAgg.Dtos;
using Usta.Domain.Core.OfferAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Entities;
using Usta.Domain.Core.OrderAgg.Enums;
using Usta.Framework;
using Usta.Infrastructure.EFCore.Persistence;

namespace Usta.Infrastructure.EFCore.Repositories.OrderAgg
{
    public class OrderRepository(AppDbContext dbContext) : IOrderRepository
    {
        public async Task<bool> Add(Order newOrder, CancellationToken cancellationToken)
        {
            dbContext.Orders.Add(newOrder);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Update(OrderEditInput input, CancellationToken cancellationToken)
        {
            var affected = await dbContext.Orders.Where(o => o.Id == input.Id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(o => o.Description, input.Description)
                    .SetProperty(o => o.StartDateTime, input.StartDateTime)
                    .SetProperty(c => c.UpdatedAt, DateTime.Now), cancellationToken);
            return affected > 0;
        }

        public async Task<OrderDto?> GetById(int id, CancellationToken cancellationToken)
        {
            return await dbContext.Orders
                .AsNoTracking()
                .Where(o => o.Id == id)
                .Select(o => new OrderDto()
                {
                    Id = o.Id,
                    CustomerFullName = o.Customer.FirstName + " " + o.Customer.LastName,
                    Description = o.Description,
                    Images = o.Images,
                    Status = o.Status,
                    StartDateTime = o.StartDateTime,
                    EndDateTime = o.EndDateTime,
                    OffersCount = o.Offers.Count,
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<OrderDto>> GetOrdersForExpert(List<int> expertServices, int? cityId, int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken)
        {
            var query = dbContext.Orders
                .AsNoTracking()
                .Where(o => expertServices.Contains(o.ProvidedServiceId) && o.Status == OrderStatus.WaitingForOffers);

            if (cityId is not null)
                query = query.Where(o => o.Customer.CityId == cityId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(o =>
                    o.ProvidedService.Title.Contains(search) ||
                    (o.ProvidedService.Description != null && o.ProvidedService.Description.Contains(search)) ||
                    o.Description.Contains(search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderDto()
                {
                    Id = o.Id,
                    CustomerFullName = o.Customer.FirstName + " " + o.Customer.LastName,
                    Description = o.Description,
                    Images = o.Images,
                    Status = o.Status,
                    StartDateTime = o.StartDateTime,
                    EndDateTime = o.EndDateTime,
                    StartShamsiDate = o.StartDateTime.ToPersianDate(),
                    EndShamsiDate = o.EndDateTime.HasValue ? o.EndDateTime.Value.ToPersianDate() : "-",
                    OffersCount = o.Offers.Count,
                }).ToListAsync(cancellationToken);

            return new PagedResult<OrderDto>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<PagedResult<OrderAndOfferDto>> GetCustomerOrders(int customerId, int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            var query = dbContext.Orders
                .AsNoTracking()
                .Where(o => o.CustomerId == customerId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(o =>
                    o.ProvidedService.Title.Contains(search) ||
                    (o.ProvidedService.Description != null && o.ProvidedService.Description.Contains(search)) ||
                    o.Description.Contains(search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderAndOfferDto()
                {
                    Id = o.Id,
                    ProvidedServiceTitle = o.ProvidedService.Title,
                    CustomerFullName = o.Customer.FirstName + " " + o.Customer.LastName,
                    Description = o.Description,
                    Images = o.Images,
                    Status = o.Status,
                    StartDateTime = o.StartDateTime,
                    EndDateTime = o.EndDateTime,
                    StartShamsiDate = o.StartDateTime.ToPersianDate(),
                    EndShamsiDate = o.EndDateTime.HasValue ? o.EndDateTime.Value.ToPersianDate() : "-",
                    OffersCount = o.Offers.Count,
                    Offers = o.Offers.Select(offer => new OfferDto
                    {
                        Id = offer.Id,
                        Description = offer.Description,
                        Price = offer.Price,
                        ExpertId = offer.ExpertId,
                        ExpertName = offer.Expert.FirstName + " " + offer.Expert.LastName,
                        ImageUrl = offer.ImageUrl,
                        IsAccepted = offer.IsAccepted,
                        StartDateTime = offer.StartDateTime
                    }).ToList()
                }).ToListAsync(cancellationToken);

            return new PagedResult<OrderAndOfferDto>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<decimal> GetPriceByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await dbContext.Orders.Where(o => o.Id == orderId)
                .Select(o => o.AcceptedOffer!.Price)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> GetExpertIdByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await dbContext.Orders.Where(o => o.Id == orderId)
                .Select(o => o.AcceptedOffer!.ExpertId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> GetCustomerIdByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await dbContext.Orders.Where(o => o.Id == orderId)
                .Select(o => o.CustomerId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> SetWaitingForPayment(int orderId, CancellationToken cancellationToken)
        {
            var affectedRow = await dbContext.Orders.Where(o => o.Id == orderId)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(o => o.Status, OrderStatus.WaitingForPayment), cancellationToken);

            return affectedRow > 0;
        }

        public async Task<bool> OrderAcceptOffer(int orderId, int offerId, CancellationToken cancellationToken)
        {
            var affectedRow = await dbContext.Orders
                .Where(o => o.Id == orderId)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(o => o.AcceptedOfferId, offerId)
                    .SetProperty(o => o.Status, OrderStatus.InProgress), cancellationToken);
            return affectedRow > 0;
        }

        public async Task<bool> CheckOrderAcceptedOffer(int orderId, CancellationToken cancellationToken)
        {
            return await dbContext.Orders
                .AnyAsync(
                    o => o.Id == orderId && o.AcceptedOfferId != null,
                    cancellationToken);
        }

        public async Task<bool> checkOrderExist(int orderId, CancellationToken cancellationToken)
        {
            return await dbContext.Orders.AnyAsync(o => o.Id == orderId, cancellationToken);
        }

        public async Task SetDone(int orderId, CancellationToken cancellationToken)
        {
            var affectedRow = await dbContext.Orders.Where(o => o.Id == orderId)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(o => o.Status, OrderStatus.Completed)
                    .SetProperty(o => o.EndDateTime, DateTime.Now), cancellationToken);
        }

        public async Task<bool> OrderIsCompleted(int orderId, CancellationToken cancellationToken)
        {
            return await dbContext.Orders
                .Where(o => o.Id == orderId)
                .AnyAsync(o => o.Status == OrderStatus.Completed, cancellationToken);
        }

        public async Task<bool> OrderHasComment(int orderId, CancellationToken cancellationToken)
        {
            return await dbContext.Orders
                .Where(o => o.Id == orderId)
                .AnyAsync(o => o.Comment != null, cancellationToken);
        }

        public async Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            var query = dbContext.Orders
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(o => !o.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(o =>
                    o.ProvidedService.Title.Contains(search) ||
                    (o.ProvidedService.Description != null && o.ProvidedService.Description.Contains(search)) ||
                    o.Description.Contains(search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                 .OrderByDescending(u => u.Id)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .Select(o => new OrderDto()
                 {
                     Id = o.Id,
                     CustomerFullName = o.Customer.FirstName + " " + o.Customer.LastName,
                     Description = o.Description,
                     Images = o.Images,
                     Status = o.Status,
                     StartDateTime = o.StartDateTime,
                     EndDateTime = o.EndDateTime,
                     StartShamsiDate = o.StartDateTime.ToPersianDate(),
                     EndShamsiDate = o.EndDateTime.HasValue ? o.EndDateTime.Value.ToPersianDate() : "-",
                     OffersCount = o.Offers.Count,
                 }).ToListAsync(cancellationToken);

            return new PagedResult<OrderDto>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}