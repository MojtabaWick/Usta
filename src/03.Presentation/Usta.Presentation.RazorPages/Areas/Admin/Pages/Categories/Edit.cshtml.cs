using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.CategoryAgg.Contracts;
using Usta.Domain.Core.CategoryAgg.Dtos;

namespace Usta.Presentation.RazorPages.Areas.Admin.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ICategoryAppService _categoryService;

        public EditModel(ICategoryAppService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public CategoryEditDto Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            Category = await _categoryService.GetForEditAsync(id, cancellationToken);
            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _categoryService.UpdateAsync(Category, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}