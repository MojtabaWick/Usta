using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Presentation.RazorPages.Extentions;

namespace Usta.Presentation.RazorPages.Areas.Customer.Pages
{
    public class PlaceOrderModel : BasePageModel
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IProvidedServiceAppService _providedServiceAppService;

        public PlaceOrderModel(IOrderAppService orderAppService, IProvidedServiceAppService providedService)
        {
            _orderAppService = orderAppService;
            _providedServiceAppService = providedService;
        }

        [BindProperty]
        public CreateOrderDto Input { get; set; }

        public PSForPlaceOrderDto? ProvidedService { get; set; }

        public async Task<IActionResult> OnGet(int serviceId, CancellationToken cancellationToken)
        {
            ProvidedService = await _providedServiceAppService
                .GetForPlaceOrder(serviceId, cancellationToken);

            if (ProvidedService == null)
                return NotFound();

            Input = new CreateOrderDto
            {
                ProvidedServiceId = serviceId
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            var customerId = (int)GetUserId()!;

            var result = await _orderAppService.CreateOrder(
                Input,
                customerId,
                cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message!);

                ProvidedService = await _providedServiceAppService
                    .GetForPlaceOrder(Input.ProvidedServiceId, cancellationToken);

                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}