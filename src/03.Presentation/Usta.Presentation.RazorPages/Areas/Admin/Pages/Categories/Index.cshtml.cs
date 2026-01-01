using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.CategoryAgg.Contracts;
using Usta.Domain.Core.CategoryAgg.Dtos;

namespace Usta.Presentation.RazorPages.Areas.Admin.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryAppService _categoryService;

        public IndexModel(ICategoryAppService categoryService)
        {
            _categoryService = categoryService;
        }

        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            Categories = await _categoryService.GetAllCategories(cancellationToken);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteAsync(id, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}