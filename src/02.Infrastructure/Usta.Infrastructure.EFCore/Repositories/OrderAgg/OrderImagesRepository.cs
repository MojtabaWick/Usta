using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Entities;
using Usta.Infrastructure.EFCore.Persistence;

namespace Usta.Infrastructure.EFCore.Repositories.OrderAgg
{
    public class OrderImagesRepository(AppDbContext dbContext) : IOrderImagesRepository
    {
        public async Task<bool> AddOrderImages(int orderId, List<OrderImage> newImages, CancellationToken cancellationToken)
        {
            var order = await dbContext.Orders.Where(o => o.Id == orderId).FirstOrDefaultAsync(cancellationToken);
            if (order is not null)
            {
                order.Images.AddRange(newImages);
                return await dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
            else
            {
                throw new Exception($"order with {orderId} not found");
            }
        }

        public async Task<bool> DeleteOrderImages(int orderId, CancellationToken cancellationToken)
        {
            var order = await dbContext.Orders.Where(o => o.Id == orderId).FirstOrDefaultAsync(cancellationToken);
            if (order is not null)
            {
                order.Images.Clear();
                return await dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
            else
            {
                throw new Exception($"order with {orderId} not found");
            }
        }
    }
}