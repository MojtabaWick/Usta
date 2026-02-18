using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.OfferAgg.Contracts;
using Usta.Domain.Core.OfferAgg.Dtos;
using Usta.Framework;
using Usta.Presentation.RazorPages.Extentions;

namespace Usta.Presentation.RazorPages.Areas.Expert.Pages.Offer
{
    public class CreateOfferModel(IOfferAppService offerAppService) : BasePageModel
    {
        [BindProperty]
        public CreateOfferDto Input { get; set; }

        public async Task<IActionResult> OnGet(int orderId, CancellationToken cancellationToken)
        {
            Input = new CreateOfferDto()
            {
                OrderId = orderId,
                ExpertId = (int)GetUserId()!,
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (Input.StartDateTime.ToGregorianDateTime() < DateTime.Now)
            {
                ModelState.AddModelError("DateError", "روز انتخابی نمیتواند گذشته باشد.");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await offerAppService.CreateOffer(Input, cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("ResultError", result.Message!);

                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}