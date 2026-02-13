using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;
using Usta.Infrastructure.Cache;
using Usta.Infrastructure.Cache.Contracts;
using Usta.Infrastructure.FileService.Contracts;

namespace Usta.Domain.Service.ProvidedServiceAgg
{
    public class ProvidedServiceService(IProvidedServiceRepository providedServiceRepository,
        IFileService _fileService,
        ICacheService _cacheService,
        IProvidedServiceDapperRepository psDapperRepository) : IProvidedServiceService
    {
        private readonly string PicFolder = "ProvidedServices";

        public async Task<List<ProfileProvidedServiceDto>> GetAllForProfileAsync(CancellationToken cancellationToken)
        {
            return await psDapperRepository.GetAllForProfileAsync(cancellationToken);
        }

        public async Task<List<ProvidedService>> GetByListIdsAsync(List<int> serviceIds, CancellationToken cancellationToken)
        {
            return await providedServiceRepository.GetByListIdsAsync(serviceIds, cancellationToken);
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllForAdmin(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            var cacheKey = $"{CacheKeys.ProvidedServices}-{pageNumber}-{search}";
            var services = _cacheService.Get<PagedResult<ProvidedServiceDto>?>(cacheKey);

            if (services is null)
            {
                services = await psDapperRepository.GetAllProvidedServiceForAdmin(pageNumber, pageSize, search, cancellationToken);
                _cacheService.Set(cacheKey, services, 10);
            }

            return services;
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllForHome(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            var cacheKey = $"{CacheKeys.ProvidedServices}-{pageNumber}-{search}";
            var services = _cacheService.Get<PagedResult<ProvidedServiceDto>?>(cacheKey);

            if (services is null)
            {
                services = await psDapperRepository.GetAllProvidedService(pageNumber, pageSize, search, cancellationToken);
                _cacheService.SetSliding(cacheKey, services, 5);
            }

            return services;
        }

        public async Task<PSForPlaceOrderDto?> GetForPlaceOrder(int providedServiceId, CancellationToken cancellationToken)
        {
            return await providedServiceRepository.GetForPlaceOrder(providedServiceId, cancellationToken);
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllProvidedServiceByCategory(int categoryId, int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"{CacheKeys.ProvidedServices}-{categoryId}-{pageNumber}-{search}";

            var services = _cacheService.Get<PagedResult<ProvidedServiceDto>?>(cacheKey);

            if (services is null)
            {
                services = await psDapperRepository.GetAllProvidedServiceByCategory(categoryId, pageNumber, pageSize,
                    search, cancellationToken);
                _cacheService.SetSliding(cacheKey, services, 5);
            }

            return services;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var result = await providedServiceRepository.DeleteAsync(id, cancellationToken);
            _cacheService.ClearByPattern(CacheKeys.ProvidedServices);
            return result;
        }

        public async Task<ProvidedServiceEditDto?> GetProvidedServiceByIdForEdit(int id, CancellationToken cancellationToken)
        {
            return await providedServiceRepository.GetProvidedServiceByIdForEdit(id, cancellationToken);
        }

        public async Task<bool> Create(CreateProvideServiceDto input, CancellationToken cancellationToken)
        {
            _cacheService.ClearByPattern(CacheKeys.ProvidedServices);

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

        public async Task<bool> UpdateProvidedService(ProvidedServiceEditDto input, CancellationToken cancellationToken)
        {
            _cacheService.ClearByPattern(CacheKeys.ProvidedServices);

            if (input.ImaFile is not null)
            {
                if (input.ImageUrl is not null)
                {
                    await _fileService.DeleteByUrlAsync(input.ImageUrl, cancellationToken);
                }

                input.ImageUrl = await _fileService.Upload(input.ImaFile, PicFolder, cancellationToken);
            }
            return await providedServiceRepository.UpdateProvidedService(input, cancellationToken);
        }
    }
}