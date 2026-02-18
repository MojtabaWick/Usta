using Microsoft.EntityFrameworkCore;
using Usta.Domain.Core._common;
using Usta.Domain.Core.OfferAgg.Contracts;
using Usta.Domain.Core.OfferAgg.Dtos;
using Usta.Domain.Core.OfferAgg.Entities;
using Usta.Domain.Core.OfferAgg.Enums;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Infrastructure.EFCore.Persistence;

namespace Usta.Infrastructure.EFCore.Repositories.OfferAgg
{
    public class OfferRepository(AppDbContext dbContext) : IOfferRepository
    {
        public async Task<bool> CreateOffer(Offer newOffer, CancellationToken cancellationToken)
        {
            dbContext.Offers.Add(newOffer);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<List<OfferDto>> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await dbContext.Offers
                  .AsNoTracking()
                  .Where(o => o.OrderId == orderId)
                  .OrderByDescending(o => o.IsAccepted)
                  .Select(o => new OfferDto()
                  {
                      Id = o.Id,
                      Description = o.Description,
                      Price = o.Price,
                      ExpertId = o.ExpertId,
                      ExpertName = o.Expert.FirstName + " " + o.Expert.LastName,
                      ImageUrl = o.ImageUrl,
                      StartDateTime = o.StartDateTime,
                      IsAccepted = o.IsAccepted,
                      Status = o.Status,
                  }).ToListAsync(cancellationToken);
        }

        public async Task<bool> CheckOfferExist(int offerId, CancellationToken cancellationToken)
        {
            return await dbContext.Offers.AnyAsync(o => o.Id == offerId, cancellationToken);
        }

        public async Task<bool> AcceptOffer(int offerId, CancellationToken cancellationToken)
        {
            var affcetedRow = await dbContext.Offers.Where(o => o.Id == offerId)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(o => o.IsAccepted, true)
                    .SetProperty(o => o.Status, OfferStatus.Accepted), cancellationToken);
            return affcetedRow > 0;
        }

        public async Task<List<int>> GetOfferIdsByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await dbContext.Offers
                .AsNoTracking()
                .Where(o => o.OrderId == orderId)
                .Select(o => o.Id).ToListAsync(cancellationToken);
        }

        public async Task<bool> RejectOffer(int offerId, CancellationToken cancellationToken)
        {
            var affcetedRow = await dbContext.Offers.Where(o => o.Id == offerId)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(o => o.Status, OfferStatus.Rejected), cancellationToken);
            return affcetedRow > 0;
        }

        public async Task<PagedResult<OfferDto>> GetExpertOffers(int expertId, int pageNumber, int pageSize,
            string? search, CancellationToken cancellationToken)
        {
            var query = dbContext.Offers
                .AsNoTracking()
                .Where(o => o.ExpertId == expertId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(o => o.Description != null && o.Description.Contains(search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OfferDto()
                {
                    Id = o.Id,
                    Description = o.Description,
                    Price = o.Price,
                    ExpertId = o.ExpertId,
                    ExpertName = o.Expert.FirstName + " " + o.Expert.LastName,
                    ImageUrl = o.ImageUrl,
                    StartDateTime = o.StartDateTime,
                    IsAccepted = o.IsAccepted,
                    Status = o.Status,
                }).ToListAsync(cancellationToken);

            return new PagedResult<OfferDto>()
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}