using Usta.Domain.Core.OfferAgg.Contracts;
using Usta.Domain.Core.OfferAgg.Dtos;
using Usta.Domain.Core.OfferAgg.Entities;
using Usta.Framework;
using Usta.Infrastructure.FileService.Contracts;

namespace Usta.Domain.Service.OfferAgg
{
    public class OfferService(IOfferRepository offerRepository, IFileService fileService) : IOfferService
    {
        public async Task<bool> CreateOffer(CreateOfferDto input, CancellationToken cancellationToken)
        {
            var startDate = input.StartDateTime.ToGregorianDateTime();

            if (input.File is not null)
            {
                input.ImageUrl = await fileService.Upload(input.File, "Offer", cancellationToken);
            }

            var newOffer = new Offer()
            {
                Description = input.Description,
                StartDateTime = startDate,
                ExpertId = input.ExpertId,
                OrderId = input.OrderId,
                ImageUrl = input.ImageUrl,
                Price = input.Price
            };

            return await offerRepository.CreateOffer(newOffer, cancellationToken);
        }

        public async Task<List<OfferDto>> GetByOrderId(int orderId, CancellationToken cancellationToken)
        {
            return await offerRepository.GetByOrderId(orderId, cancellationToken);
        }

        public async Task<bool> CheckOfferExist(int offerId, CancellationToken cancellationToken)
        {
            return await offerRepository.CheckOfferExist(offerId, cancellationToken);
        }

        public async Task<bool> AcceptOffer(int offerId, CancellationToken cancellationToken)
        {
            return await offerRepository.AcceptOffer(offerId, cancellationToken);
        }
    }
}