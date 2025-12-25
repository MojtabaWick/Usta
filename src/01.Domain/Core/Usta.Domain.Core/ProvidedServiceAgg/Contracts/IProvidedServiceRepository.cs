using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;

namespace Usta.Domain.Core.ProvidedServiceAgg.Contracts
{
    public interface IProvidedServiceRepository
    {
        public Task<bool> CreateProvidedService(ProvidedService input, CancellationToken cancellationToken);

        public Task<List<ProvidedServiceDto>> GetAllProvidedService(int pageNumber, int pageSize, CancellationToken cancellationToken);

        public Task<ProvidedServiceDto?> GetProvidedServiceById(int id, CancellationToken cancellationToken);

        public Task<bool> UpdateProvidedService(ProvidedServiceDto input, CancellationToken cancellationToken);
    }
}