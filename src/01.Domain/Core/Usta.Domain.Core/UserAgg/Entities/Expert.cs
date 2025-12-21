using Usta.Domain.Core._common;
using Usta.Domain.Core.CommentAgg.Entities;
using Usta.Domain.Core.OfferAgg.Entities;
using Usta.Domain.Core.ProvidedServiceAgg.Entities;

namespace Usta.Domain.Core.UserAgg.Entities
{
    public class Expert : ApplicationUser
    {
        public List<Offer> Offers { get; set; } = [];
        public List<Comment> Comments { get; set; } = [];
        public List<ProvidedService> ProvidedServices { get; set; } = [];
    }
}