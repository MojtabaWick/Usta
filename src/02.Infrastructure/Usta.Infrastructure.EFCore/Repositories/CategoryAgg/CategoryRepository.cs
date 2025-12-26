using Microsoft.EntityFrameworkCore;
using Usta.Domain.Core.CategoryAgg.Contracts;
using Usta.Domain.Core.CategoryAgg.Dtos;
using Usta.Domain.Core.CategoryAgg.Entities;
using Usta.Infrastructure.EFCore.Persistence;

namespace Usta.Infrastructure.EFCore.Repositories.CategoryAgg
{
    public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
    {
        public async Task<bool> Add(Category newCategory, CancellationToken cancellationToken)
        {
            dbContext.Categories.Add(newCategory);
            return await dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> Update(CategoryEditDto input, CancellationToken cancellationToken)
        {
            var affectedRow = await dbContext.Categories.Where(c => c.Id == input.Id)
                .ExecuteUpdateAsync(setter => setter
                        .SetProperty(c => c.Title, input.Title)
                        .SetProperty(c => c.Description, input.Description)
                        .SetProperty(c => c.ImagedUrl, input.ImagedUrl)
                    , cancellationToken);

            return affectedRow > 0;
        }

        public async Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken)
        {
            return await dbContext.Categories.Where(c => c.Id == id).Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
                ImagedUrl = c.ImagedUrl,
                Description = c.Description,
            }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
        {
            return await dbContext.Categories.Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ImagedUrl = c.ImagedUrl,
            }).ToListAsync(cancellationToken);
        }
    }
}