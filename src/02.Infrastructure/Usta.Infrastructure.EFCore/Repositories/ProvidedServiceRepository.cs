using Microsoft.EntityFrameworkCore;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;
using Usta.Infrastructure.EFCore.Persistence;

namespace Usta.Infrastructure.EFCore.Repositories
{
    public class ProvidedServiceRepository(AppDbContext dbContext) : IProvidedServiceRepository
    {
        public async Task<bool> CreateProvidedService(ProvidedService input, CancellationToken cancellationToken)
        {
            // input dto and Pictures will be handled in service.

            await dbContext.AddAsync(input, cancellationToken);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<List<ProvidedServiceDto>> GetAllProvidedService(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await dbContext.ProvidedServices
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProvidedServiceDto()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CategoryName = p.Category.Title,
                    ImageUrl = p.ImageUrl,
                    MinPrice = p.MinPrice
                }).ToListAsync(cancellationToken);
        }

        public async Task<ProvidedServiceDto?> GetProvidedServiceById(int id, CancellationToken cancellationToken)
        {
            return await dbContext.ProvidedServices
                .AsNoTracking()
                .Select(p => new ProvidedServiceDto()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Title,
                    ImageUrl = p.ImageUrl,
                    MinPrice = p.MinPrice
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> UpdateProvidedService(ProvidedServiceDto input, CancellationToken cancellationToken)
        {
            // image change will be handled in service.

            var affected = await dbContext.ProvidedServices
                .Where(p => p.Id == input.Id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(p => p.Title, input.Title)
                    .SetProperty(p => p.Description, input.Description)
                    .SetProperty(p => p.CategoryId, input.CategoryId)
                    .SetProperty(p => p.ImageUrl, input.ImageUrl)
                    .SetProperty(p => p.MinPrice, input.MinPrice),
                cancellationToken);

            return affected > 0;
        }
    }
}