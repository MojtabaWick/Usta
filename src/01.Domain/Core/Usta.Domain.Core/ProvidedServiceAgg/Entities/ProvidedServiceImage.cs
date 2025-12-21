using Usta.Domain.Core._common;

namespace Usta.Domain.Core.ProvidedServiceAgg.Entities
{
    public class ProvidedServiceImage : BaseEntity
    {
        public string ImageUrl { get; set; }

        public ProvidedService ProvidedService { get; set; }
        public int ProvidedServiceId { get; set; }
    }
}