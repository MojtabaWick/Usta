using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;
using Usta.Infrastructure.EFCore.Persistence;

namespace Usta.Infrastructure.EFCore.Repositories.ProvidedServiceAgg
{
    public class ProvidedServiceRepository(AppDbContext dbContext) : IProvidedServiceRepository
    {
        public async Task<bool> CreateProvidedService(ProvidedService input, CancellationToken cancellationToken)
        {
            // input dto and Pictures will be handled in service.

            await dbContext.AddAsync(input, cancellationToken);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<List<ProfileProvidedServiceDto>> GetAllForProfileAsync(CancellationToken cancellationToken)
        {
            return await dbContext.ProvidedServices
                .AsNoTracking()
                .Select(p => new ProfileProvidedServiceDto
                {
                    Id = p.Id,
                    Title = p.Title,
                }).ToListAsync(cancellationToken);
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllProvidedService(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            var query = dbContext.ProvidedServices
                .AsNoTracking().Where(x => !x.Category.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    p.Title.Contains(search) ||
                    p.Description != null && p.Description.Contains(search) ||
                    p.Category.Title.Contains(search)
                );
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProvidedServiceDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CategoryName = p.Category.Title,
                    ImageUrl = p.ImageUrl,
                    MinPrice = p.MinPrice
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<ProvidedServiceDto>()
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllProvidedServiceForAdmin(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            var query = dbContext.ProvidedServices
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(x => !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    p.Title.Contains(search) ||
                    p.Description != null && p.Description.Contains(search) ||
                    p.Category.Title.Contains(search)
                );
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProvidedServiceDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CategoryName = p.Category.Title,
                    ImageUrl = p.ImageUrl,
                    MinPrice = p.MinPrice
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<ProvidedServiceDto>()
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllProvidedServiceByCategory(int categoryId, int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken)
        {
            var query = dbContext.ProvidedServices
                .AsNoTracking().Where(ps => ps.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    p.Title.Contains(search) ||
                    p.Description != null && p.Description.Contains(search) ||
                    p.Category.Title.Contains(search)
                );
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProvidedServiceDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CategoryName = p.Category.Title,
                    ImageUrl = p.ImageUrl,
                    MinPrice = p.MinPrice
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<ProvidedServiceDto>()
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PSForPlaceOrderDto?> GetForPlaceOrder(int providedServiceId, CancellationToken cancellationToken)
        {
            return await dbContext.ProvidedServices
                .AsNoTracking()
                .Select(ps => new PSForPlaceOrderDto()
                {
                    Id = ps.Id,
                    Title = ps.Title,
                })
                .FirstOrDefaultAsync(ps => ps.Id == providedServiceId);
        }

        public async Task<ProvidedServiceEditDto?> GetProvidedServiceByIdForEdit(int id, CancellationToken cancellationToken)
        {
            return await dbContext.ProvidedServices
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProvidedServiceEditDto()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    ImageUrl = p.ImageUrl,
                    MinPrice = p.MinPrice
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> UpdateProvidedService(ProvidedServiceEditDto input, CancellationToken cancellationToken)
        {
            // image change will be handled in service.

            var affected = await dbContext.ProvidedServices
                .Where(p => p.Id == input.Id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(p => p.Title, input.Title)
                    .SetProperty(p => p.Description, input.Description)
                    .SetProperty(p => p.CategoryId, input.CategoryId)
                    .SetProperty(p => p.ImageUrl, input.ImageUrl)
                    .SetProperty(p => p.MinPrice, input.MinPrice)
                    .SetProperty(c => c.UpdatedAt, DateTime.Now),
                cancellationToken);

            return affected > 0;
        }

        public async Task<List<ProvidedService>> GetByListIdsAsync(List<int> serviceIds, CancellationToken cancellationToken)
        {
            if (serviceIds is null || serviceIds.Count == 0)
                return [];

            return await dbContext.ProvidedServices
                .Where(s => serviceIds.Contains(s.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var affectedRow = await dbContext.ProvidedServices.Where(ps => ps.Id == id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(ps => ps.IsDeleted, true)
                    .SetProperty(ps => ps.DeletedAt, DateTime.Now), cancellationToken);
            return affectedRow > 0;
        }
    }
}