using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.CategoryAgg.Contracts;
using Usta.Domain.Core.CategoryAgg.Dtos;

namespace Usta.Presentation.RazorPages.Areas.Admin.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryAppService _categoryService;

        public CreateModel(ICategoryAppService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public CategoryCreateDto Category { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _categoryService.CreateAsync(Category, cancellationToken);

            if (result.IsSuccess)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError("", result.Message!);
                return Page();
            }
        }
    }
}