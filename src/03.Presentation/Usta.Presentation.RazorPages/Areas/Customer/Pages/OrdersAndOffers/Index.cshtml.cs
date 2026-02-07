using Microsoft.AspNetCore.Mvc;
using Usta.Domain.Core._common;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Entities;
using Usta.Presentation.RazorPages.Extentions;

namespace Usta.Presentation.RazorPages.Areas.Customer.Pages.OrdersAndOffers
{
    public class IndexModel : BasePageModel
    {
        private readonly IOrderAppService _orderAppService;

        public IndexModel(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public PagedResult<OrderAndOfferDto> OrdersAndOffers { get; set; }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            OrdersAndOffers = await _orderAppService.GetCustomerOrders((int)GetUserId()!, PageNumber, 10, Search, cancellationToken);
        }

        public async Task<IActionResult> OnPostAcceptOfferAsync(int orderId, int offerId, CancellationToken cancellationToken)
        {
            await _orderAppService.AcceptOffer(orderId, offerId, cancellationToken);
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostSetDoneAsync(int orderId, CancellationToken cancellationToken)
        {
            await _orderAppService.SetOrderDone(orderId, cancellationToken);
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostPayOrderAsync(int orderId, CancellationToken cancellationToken)
        {
            await _orderAppService.PayOrder(orderId, cancellationToken);
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostCreateCommentAsync(int orderId, CancellationToken cancellationToken)
        {
            return RedirectToPage("/Comments/CreateComment", new { area = "Customer", orderId });
        }
    }
}