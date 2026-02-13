using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.OfferAgg.Dtos;
using Usta.Domain.Core.OfferAgg.Contracts;
using Usta.Presentation.RazorPages.Extentions;

namespace Usta.Presentation.RazorPages.Areas.Expert.Pages.Offer
{
    public class MyOffersModel : BasePageModel
    {
        private readonly IOfferAppService _offerService;

        public MyOffersModel(IOfferAppService offerAppService)
        {
            _offerService = offerAppService;
        }

        public List<OfferDto> Offers { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }

        private const int PageSize = 10;

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            int expertId = (int)GetUserId()!;

            var result = await _offerService.GetExpertOffers(
                expertId,
                PageNumber,
                PageSize,
                Search,
                cancellationToken);

            Offers = result.Items;
            TotalPages = (int)Math.Ceiling(result.TotalCount / (double)PageSize);
        }
    }
}