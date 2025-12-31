using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;

namespace Usta.Domain.Core.ProvidedServiceAgg.Contracts
{
    public interface IProvidedServiceService
    {
        public Task<List<ProfileProvidedServiceDto>> GetAllForProfileAsync(CancellationToken cancellationToken);

        public Task<List<ProvidedService>> GetByListIdsAsync(List<int> serviceIds, CancellationToken cancellationToken);
    }
}