using Usta.Domain.Core.CategoryAgg.Dtos;
using Usta.Domain.Core.CategoryAgg.Entities;
using Usta.Domain.Core.CityAgg.Entities;

namespace Usta.Domain.Core.CategoryAgg.Contracts
{
    public interface ICategoryRepository
    {
        public Task<bool> Add(Category newCategory, CancellationToken cancellationToken);

        public Task<bool> Update(CategoryEditDto input, CancellationToken cancellationToken);

        public Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken);

        public Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken);
    }
}