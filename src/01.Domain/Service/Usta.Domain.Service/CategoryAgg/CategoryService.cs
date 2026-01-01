using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CategoryAgg.Contracts;
using Usta.Domain.Core.CategoryAgg.Dtos;
using Usta.Domain.Core.CategoryAgg.Entities;
using Usta.Infrastructure.FileService.Contracts;

namespace Usta.Domain.Service.CategoryAgg
{
    public class CategoryService(ICategoryRepository categoryRepo, IFileService fileService) : ICategoryService
    {
        private readonly string _categoriesFolder = "Categories";

        public async Task<bool> CreateAsync(CategoryCreateDto input, CancellationToken cancellationToken)
        {
            input.ImagedUrl = await fileService.Upload(input.ImagFile, _categoriesFolder, cancellationToken);

            var newCategory = new Category()
            {
                Title = input.Title,
                Description = input.Description,
                ImagedUrl = input.ImagedUrl,
            };

            return await categoryRepo.Add(newCategory, cancellationToken);
        }

        public async Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
        {
            return await categoryRepo.GetAllCategories(cancellationToken);
        }

        public async Task<CategoryEditDto> GetForEditAsync(int id, CancellationToken cancellationToken)
        {
            var category = await categoryRepo.GetById(id, cancellationToken);
            if (category is null)
            {
                throw new Exception($"Category with id: {id} not found.");
            }

            var categoryEditDto = new CategoryEditDto()
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description,
                ImagedUrl = category.ImagedUrl
            };

            return categoryEditDto;
        }

        public async Task<bool> UpdateAsync(CategoryEditDto input, CancellationToken cancellationToken)
        {
            if (input.ImageFile is not null)
            {
                await fileService.DeleteByUrlAsync(input.ImagedUrl, cancellationToken);
                input.ImagedUrl = await fileService.Upload(input.ImageFile, _categoriesFolder, cancellationToken);
            }

            return await categoryRepo.Update(input, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await categoryRepo.DeleteAsync(id, cancellationToken);
        }
    }
}