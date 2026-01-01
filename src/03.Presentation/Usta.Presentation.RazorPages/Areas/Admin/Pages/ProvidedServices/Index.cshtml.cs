using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;

namespace Usta.Presentation.RazorPages.Areas.Admin.Pages.ProvidedServices
{
    public class IndexModel : PageModel
    {
        private readonly IProvidedServiceAppService _providedServiceApp;

        public IndexModel(IProvidedServiceAppService service)
        {
            _providedServiceApp = service;
        }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public PagedResult<ProvidedServiceDto> Services { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Services = await _providedServiceApp.GetAllForAdmin(PageNumber, 10, Search, cancellationToken
            );
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _providedServiceApp.DeleteAsync(id, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}