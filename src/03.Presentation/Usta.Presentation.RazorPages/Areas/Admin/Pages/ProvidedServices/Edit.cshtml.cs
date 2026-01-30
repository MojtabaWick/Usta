using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.CategoryAgg.Contracts;
using Usta.Domain.Core.CategoryAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;

namespace Usta.Presentation.RazorPages.Areas.Admin.Pages.ProvidedServices
{
    public class EditModel : PageModel
    {
        private readonly IProvidedServiceAppService _pSAppService;
        private readonly ICategoryAppService _categoryAppService;

        public EditModel(IProvidedServiceAppService pSAppService, ICategoryAppService categoryAppService)
        {
            _pSAppService = pSAppService;
            _categoryAppService = categoryAppService;
        }

        [BindProperty]
        public ProvidedServiceEditDto ProvidedServiceEditDto { get; set; }

        public List<CategorySelectDto> CategorySelect { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            ProvidedServiceEditDto = await _pSAppService.GetProvidedServiceByIdForEdit(id, cancellationToken);
            if (ProvidedServiceEditDto is null)
            {
                return NotFound();
            }
            await LoadCategoriesAsync(cancellationToken);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync(cancellationToken);
                return Page();
            }

            await _pSAppService.UpdateProvidedService(ProvidedServiceEditDto!, cancellationToken);
            return RedirectToPage("Index");
        }

        private async Task LoadCategoriesAsync(CancellationToken cancellationToken)
        {
            CategorySelect = await _categoryAppService.GetCategoriesForSelect(cancellationToken);
        }
    }
}