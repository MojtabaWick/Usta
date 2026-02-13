using Usta.Domain.Core.CategoryAgg.Dtos;

namespace Usta.Domain.Core.CategoryAgg.Contracts
{
    public interface ICategoryDapperRepository
    {
        public Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken);

        public Task<List<CategorySelectDto>> GetCategoriesForSelect(CancellationToken cancellationToken);
    }
}