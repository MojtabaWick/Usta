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

        public async Task<bool> Create(CreateProvideServiceDto input, CancellationToken cancellationToken)
        {
            return await providedServiceService.Create(input, cancellationToken);
        }
    }
}