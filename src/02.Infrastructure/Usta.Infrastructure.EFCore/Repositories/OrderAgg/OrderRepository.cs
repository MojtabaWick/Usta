using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Dtos;
using Usta.Domain.Core.OrderAgg.Entities;
using Usta.Infrastructure.EFCore.Persistence;

namespace Usta.Infrastructure.EFCore.Repositories.OrderAgg
{
    internal class OrderRepository(AppDbContext dbContext) : IOrderRepository
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
                    Description = o.Description,
                    Images = o.Images,
                    Status = o.Status,
                    StartDateTime = o.StartDateTime,
                    EndDateTime = o.EndDateTime,
                    OffersCount = o.Offers.Count,
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<OrderDto>> GetAllOrders(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            var query = dbContext.Orders
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(o =>
                    o.ProvidedService.Title.Contains(search) ||
                    o.ProvidedService.Description != null && o.ProvidedService.Description.Contains(search) ||
                    o.Description.Contains(search));
            }

            return await query
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderDto()
                {
                    Id = o.Id,
                    Description = o.Description,
                    Images = o.Images,
                    Status = o.Status,
                    StartDateTime = o.StartDateTime,
                    EndDateTime = o.EndDateTime,
                    OffersCount = o.Offers.Count,
                }).ToListAsync(cancellationToken);
        }
    }
}