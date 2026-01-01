using Microsoft.Extensions.Logging;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CategoryAgg.Contracts;
using Usta.Domain.Core.CategoryAgg.Dtos;

namespace Usta.Domain.AppService.CategoryAgg
{
    public class CategoryAppService(ICategoryService categoryService, ILogger<CategoryAppService> _logger) : ICategoryAppService
    {
        public async Task<Result<bool>> CreateAsync(CategoryCreateDto input, CancellationToken cancellationToken)
        {
            var result = await categoryService.CreateAsync(input, cancellationToken);

            if (result)
            {
                _logger.LogInformation($"new Category with title : {input.Title} created.");
            }

            return result ? Result<bool>.Success("دسته بندی جدید با موفقیت ایجاد شد.")
                : Result<bool>.Failure("ایجاد دسته بندی با مشکل مواجه شده است.");
        }

        public async Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
        {
            return await categoryService.GetAllCategories(cancellationToken);
        }

        public async Task<CategoryEditDto> GetForEditAsync(int id, CancellationToken cancellationToken)
        {
            return await categoryService.GetForEditAsync(id, cancellationToken);
        }

        public async Task<List<CategorySelectDto>> GetCategoriesForSelect(CancellationToken cancellationToken)
        {
            return await categoryService.GetCategoriesForSelect(cancellationToken);
        }

        public async Task<bool> UpdateAsync(CategoryEditDto input, CancellationToken cancellationToken)
        {
            var result = await categoryService.UpdateAsync(input, cancellationToken);

            if (result)
            {
                _logger.LogInformation($"Category with id:{input.Id} updated.");
            }
            else
            {
                _logger.LogError($"there is a problem in updating Category with id:{input.Id}.");
            }

            return result;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var result = await categoryService.DeleteAsync(id, cancellationToken);

            if (result)
            {
                _logger.LogInformation($"category with id:{id} deleted.");
            }
            else
            {
                _logger.LogError($"couldn't delete category with id:{id}.");
            }

            return result;
        }
    }
}