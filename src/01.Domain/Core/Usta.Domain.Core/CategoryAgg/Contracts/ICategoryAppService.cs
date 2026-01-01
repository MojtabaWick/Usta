using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CategoryAgg.Dtos;

namespace Usta.Domain.Core.CategoryAgg.Contracts
{
    public interface ICategoryAppService
    {
        Task<Result<bool>> CreateAsync(CategoryCreateDto input, CancellationToken cancellationToken);

        Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken);

        Task<CategoryEditDto> GetForEditAsync(int id, CancellationToken cancellationToken);

        Task<List<CategorySelectDto>> GetCategoriesForSelect(CancellationToken cancellationToken);

        Task<bool> UpdateAsync(CategoryEditDto input, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}