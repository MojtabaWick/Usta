using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;

namespace Usta.Domain.Core.ProvidedServiceAgg.Contracts
{
    public interface IProvidedServiceDapperRepository
    {
        Task<List<ProfileProvidedServiceDto>> GetAllForProfileAsync(CancellationToken cancellationToken);

        Task<PagedResult<ProvidedServiceDto>> GetAllProvidedService(int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken);

        public Task<PagedResult<ProvidedServiceDto>> GetAllProvidedServiceForAdmin(int pageNumber, int pageSize,
            string? search, CancellationToken cancellationToken);

        Task<PagedResult<ProvidedServiceDto>> GetAllProvidedServiceByCategory(int categoryId, int pageNumber, int pageSize, string? search,
            CancellationToken cancellationToken);
    }
}