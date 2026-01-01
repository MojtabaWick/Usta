using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;

namespace Usta.Domain.Service.ProvidedServiceAgg
{
    public class ProvidedServiceService(IProvidedServiceRepository providedServiceRepository) : IProvidedServiceService
    {
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
    }
}