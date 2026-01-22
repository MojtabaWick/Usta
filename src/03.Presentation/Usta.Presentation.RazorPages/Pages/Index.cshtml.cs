using Microsoft.AspNetCore.Mvc;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CategoryAgg.Contracts;
using Usta.Domain.Core.CategoryAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Presentation.RazorPages.Extentions;

namespace Usta.Presentation.RazorPages.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly IProvidedServiceAppService _providedServiceAppService;
        private readonly ICategoryAppService _categoryAppService;

        public List<CategoryDto> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public PagedResult<ProvidedServiceDto> Services { get; set; }

        public IndexModel(IProvidedServiceAppService productAppService,
            ICategoryAppService categoryAppService)
        {
            _providedServiceAppService = productAppService;
            _categoryAppService = categoryAppService;
        }

        public async Task<IActionResult> OnGetAsync(int? categoryId, CancellationToken cancellationToken)
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToPage("/Index", new { area = "Admin" });
            }

            Categories = await _categoryAppService.GetAllCategories(cancellationToken);

            if (categoryId.HasValue)
            {
                Services = await _providedServiceAppService
                    .GetAllProvidedServiceByCategory(categoryId.Value, PageNumber, 10, Search, cancellationToken);

                SelectedCategoryId = categoryId.Value;
            }
            else
            {
                Services = await _providedServiceAppService
                    .GetAllForHome(PageNumber, 10, Search, cancellationToken);
            }

            return Page();
        }
    }
}