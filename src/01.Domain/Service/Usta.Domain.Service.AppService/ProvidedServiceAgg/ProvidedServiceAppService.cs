using Microsoft.Extensions.Logging;
using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;

namespace Usta.Domain.AppService.ProvidedServiceAgg
{
    public class ProvidedServiceAppService(IProvidedServiceService providedServiceService, ILogger<ProvidedServiceAppService> _logger) : IProvidedServiceAppService
    {
        public async Task<List<ProfileProvidedServiceDto>> GetAllForProfileAsync(CancellationToken cancellationToken)
        {
            return await providedServiceService.GetAllForProfileAsync(cancellationToken);
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllForAdmin(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            return await providedServiceService.GetAllForAdmin(pageNumber, pageSize, search, cancellationToken);
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllForHome(int pageNumber, int pageSize, string? search, CancellationToken cancellationToken)
        {
            return await providedServiceService.GetAllForHome(pageNumber, pageSize, search, cancellationToken);
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllProvidedServiceByCategory(int categoryId, int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken)
        {
            return await providedServiceService.GetAllProvidedServiceByCategory(categoryId, pageNumber, pageSize,
                search, cancellationToken);
        }

        public async Task<PSForPlaceOrderDto?> GetForPlaceOrder(int providedServiceId, CancellationToken cancellationToken)
        {
            return await providedServiceService.GetForPlaceOrder(providedServiceId, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var result = await providedServiceService.DeleteAsync(id, cancellationToken);
            if (result)
            {
                _logger.LogInformation($"provided service with id:{id} deleted successfully.");
            }
            else
            {
                _logger.LogError($"there is a problem deleting provided service with id:{id}.");
            }

            return result;
        }

        public async Task<ProvidedServiceEditDto?> GetProvidedServiceByIdForEdit(int id, CancellationToken cancellationToken)
        {
            var pSEditDto = await providedServiceService.GetProvidedServiceByIdForEdit(id, cancellationToken);

            if (pSEditDto is null)
            {
                _logger.LogError($"provide service with id:{id} not found.");
            }

            return pSEditDto;
        }

        public async Task<bool> Create(CreateProvideServiceDto input, CancellationToken cancellationToken)
        {
            var result = await providedServiceService.Create(input, cancellationToken);
            if (result)
            {
                _logger.LogInformation($"new provided service with title:{input.Title} created.");
            }
            return result;
        }

        public async Task<bool> UpdateProvidedService(ProvidedServiceEditDto input, CancellationToken cancellationToken)
        {
            var result = await providedServiceService.UpdateProvidedService(input, cancellationToken);
            if (result)
            {
                _logger.LogInformation($"provided service with id:{input.Id} updated.");
            }

            return result;
        }
    }
}