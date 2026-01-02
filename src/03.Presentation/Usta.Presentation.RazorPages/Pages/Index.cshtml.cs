using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Presentation.RazorPages.Extentions;

namespace Usta.Presentation.RazorPages.Pages
{
    public class IndexModel : BasePageModel
    {
        public IActionResult OnGet()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToPage("/Index", new { area = "Admin" });
            }

            return Page();
        }
    }
}