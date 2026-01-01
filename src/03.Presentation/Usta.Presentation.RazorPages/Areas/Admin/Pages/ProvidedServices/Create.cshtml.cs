using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Usta.Domain.Core.CategoryAgg.Contracts;
using Usta.Domain.Core.CategoryAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;

namespace Usta.Presentation.RazorPages.Areas.Admin.Pages.ProvidedServices
{
    public class CreateModel : PageModel
    {
        private readonly IProvidedServiceAppService _pSAppService;
        private readonly ICategoryAppService _categoryAppService;

        public CreateModel(
            IProvidedServiceAppService pSAppService,
            ICategoryAppService categoryService)
        {
            _pSAppService = pSAppService;
            _categoryAppService = categoryService;
        }

        [BindProperty]
        public CreateProvideServiceDto Service { get; set; }

        public List<CategorySelectDto> Categories { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Categories = await _categoryAppService.GetCategoriesForSelect(cancellationToken);
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                Categories = await _categoryAppService.GetCategoriesForSelect(cancellationToken);
                return Page();
            }

            await _pSAppService.Create(Service, cancellationToken);

            return RedirectToPage("Index");
        }
    }
}