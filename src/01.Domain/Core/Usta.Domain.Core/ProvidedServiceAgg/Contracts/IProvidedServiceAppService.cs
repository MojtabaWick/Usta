using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;

namespace Usta.Domain.Core.ProvidedServiceAgg.Contracts
{
    public interface IProvidedServiceAppService
    {
        Task<List<ProfileProvidedServiceDto>> GetAllForProfileAsync(CancellationToken cancellationToken);

        Task<PagedResult<ProvidedServiceDto>> GetAllForAdmin(int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken);

        Task<PagedResult<ProvidedServiceDto>> GetAllForHome(int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken);

        Task<PagedResult<ProvidedServiceDto>> GetAllProvidedServiceByCategory(int categoryId, int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken);

        Task<PSForPlaceOrderDto?> GetForPlaceOrder(int providedServiceId, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<ProvidedServiceEditDto?> GetProvidedServiceByIdForEdit(int id, CancellationToken cancellationToken);

        Task<bool> Create(CreateProvideServiceDto input, CancellationToken cancellationToken);

        Task<bool> UpdateProvidedService(ProvidedServiceEditDto input, CancellationToken cancellationToken);
    }
}