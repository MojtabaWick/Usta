using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CommentAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Entities;
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

        public async Task<PagedResult<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            var query = dbContext.Orders
                .AsNoTracking();

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