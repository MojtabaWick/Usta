using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;
using Usta.Infrastructure.FileService.Contracts;

namespace Usta.Domain.Service.ProvidedServiceAgg
{
    public class ProvidedServiceService(IProvidedServiceRepository providedServiceRepository, IFileService _fileService) : IProvidedServiceService
    {
        private readonly string PicFolder = "ProvidedServices";

        public async Task<List<ProfileProvidedServiceDto>> GetAllForProfileAsync(CancellationToken cancellationToken)
        {
            return await providedServiceRepository.GetAllForProfileAsync(cancellationToken);
        }

        public async Task<List<ProvidedService>> GetByListIdsAsync(List<int> serviceIds, CancellationToken cancellationToken)
        {
            return await providedServiceRepository.GetByListIdsAsync(serviceIds, cancellationToken);
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllForAdmin(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            return await providedServiceRepository.GetAllProvidedService(pageNumber, pageSize, search,
                cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            return await providedServiceRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<bool> Create(CreateProvideServiceDto input, CancellationToken cancellationToken)
        {
            if (input.ImageFile is not null)
            {
                input.ImageUrl = await _fileService.Upload(input.ImageFile, PicFolder, cancellationToken);
            }

            var newProvidedService = new ProvidedService()
            {
                Title = input.Title,
                Description = input.Description,
                CategoryId = input.CategoryId,
                MinPrice = input.MinPrice,
                ImageUrl = input.ImageUrl
            };

            return await providedServiceRepository.CreateProvidedService(newProvidedService, cancellationToken);
        }
    }
}