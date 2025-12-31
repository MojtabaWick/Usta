using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;

namespace Usta.Domain.AppService.ProvidedServiceAgg
{
    public class ProvidedServiceAppService(IProvidedServiceService providedServiceService) : IProvidedServiceAppService
    {
        public async Task<List<ProfileProvidedServiceDto>> GetAllForProfileAsync(CancellationToken cancellationToken)
        {
            return await providedServiceService.GetAllForProfileAsync(cancellationToken);
        }
    }
}