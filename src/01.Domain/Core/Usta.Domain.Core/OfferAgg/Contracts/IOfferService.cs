using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Usta.Domain.Core._common;
using Usta.Domain.Core.OfferAgg.Dtos;
using Usta.Domain.Core.OfferAgg.Entities;
using Usta.Domain.Core.OrderAgg.Dtos;

namespace Usta.Domain.Core.OfferAgg.Contracts
{
    public interface IOfferService
    {
        public Task<bool> CreateOffer(CreateOfferDto input, CancellationToken cancellationToken);

        public Task<List<OfferDto>> GetByOrderId(int orderId, CancellationToken cancellationToken);

        public Task<bool> CheckOfferExist(int offerId, CancellationToken cancellationToken);

        public Task<bool> AcceptOffer(int offerId, CancellationToken cancellationToken);

        public Task<PagedResult<OfferDto>> GetExpertOffers(int expertId, int pageNumber, int pageSize, string? search, CancellationToken cancellationToken);
    }
}