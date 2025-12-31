using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;

namespace Usta.Domain.Service.ProvidedServiceAgg
{
    public class ProvidedServiceService(IProvidedServiceRepository providedServiceRepository) : IProvidedServiceService
    {
        public async Task<List<ProfileProvidedServiceDto>> GetAllForProfileAsync(CancellationToken cancellationToken)
        {
            return await providedServiceRepository.GetAllForProfileAsync(cancellationToken);
        }
    }
}