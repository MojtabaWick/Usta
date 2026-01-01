using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;

namespace Usta.Domain.Core.ProvidedServiceAgg.Contracts
{
    public interface IProvidedServiceRepository
    {
        public Task<bool> CreateProvidedService(ProvidedService input, CancellationToken cancellationToken);

        public Task<List<ProfileProvidedServiceDto>> GetAllForProfileAsync(CancellationToken cancellationToken);

        public Task<PagedResult<ProvidedServiceDto>> GetAllProvidedService(int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken);

        public Task<ProvidedServiceDto?> GetProvidedServiceById(int id, CancellationToken cancellationToken);

        public Task<bool> UpdateProvidedService(ProvidedServiceEditDto input, CancellationToken cancellationToken);

        public Task<List<ProvidedService>> GetByListIdsAsync(List<int> serviceIds, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}