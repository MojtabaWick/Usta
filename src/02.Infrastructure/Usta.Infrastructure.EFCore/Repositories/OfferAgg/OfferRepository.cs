using Microsoft.EntityFrameworkCore;
using Usta.Domain.Core.OfferAgg.Contracts;
using Usta.Domain.Core.OfferAgg.Dtos;
using Usta.Infrastructure.EFCore.Persistence;

namespace Usta.Infrastructure.EFCore.Repositories.OfferAgg
{
    public class OfferRepository(AppDbContext dbContext) : IOfferRepository
    {
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
                      IsAccepted = o.IsAccepted
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
                    .SetProperty(o => o.IsAccepted, true), cancellationToken);
            return affcetedRow > 0;
        }
    }
}