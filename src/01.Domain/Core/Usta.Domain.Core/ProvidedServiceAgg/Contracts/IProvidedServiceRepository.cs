using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;

namespace Usta.Domain.Core.ProvidedServiceAgg.Contracts
{
    public interface IProvidedServiceRepository
    {
        Task<bool> CreateProvidedService(ProvidedService input, CancellationToken cancellationToken);

        Task<List<ProfileProvidedServiceDto>> GetAllForProfileAsync(CancellationToken cancellationToken);

        Task<PagedResult<ProvidedServiceDto>> GetAllProvidedService(int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken);

        Task<PagedResult<ProvidedServiceDto>> GetAllProvidedServiceByCategory(int categoryId, int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken);

        Task<PSForPlaceOrderDto?> GetForPlaceOrder(int providedServiceId, CancellationToken cancellationToken);

        Task<ProvidedServiceEditDto?> GetProvidedServiceByIdForEdit(int id, CancellationToken cancellationToken);

        Task<bool> UpdateProvidedService(ProvidedServiceEditDto input, CancellationToken cancellationToken);

        Task<List<ProvidedService>> GetByListIdsAsync(List<int> serviceIds, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}