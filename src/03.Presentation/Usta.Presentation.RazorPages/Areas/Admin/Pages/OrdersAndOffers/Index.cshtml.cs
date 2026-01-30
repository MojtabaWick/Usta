using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core._common;
using Usta.Domain.Core.OfferAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Dtos;

namespace Usta.Presentation.RazorPages.Areas.Admin.Pages.OrdersAndOffers
{
    public class IndexModel : PageModel
    {
        private readonly IOrderAppService _orderService;
        private readonly IOfferAppService _offerService;

        public IndexModel(
            IOrderAppService orderService,
            IOfferAppService offerService)
        {
            _orderService = orderService;
            _offerService = offerService;
        }

        public PagedResult<OrderDto> Orders { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        private const int PageSize = 10;

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Orders = await _orderService.GetAllOrders(
                PageNumber,
                PageSize,
                Search,
                cancellationToken);
        }

        // Ajax Handler
        public async Task<IActionResult> OnGetOffers(
            int orderId,
            CancellationToken cancellationToken)
        {
            var offers = await _offerService.GetByOrderId(orderId, cancellationToken);
            return new JsonResult(offers);
        }
    }
}