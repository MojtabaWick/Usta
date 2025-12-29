using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.UserAgg.Contracts;

namespace Usta.Presentation.RazorPages.Areas.Account.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IUserAppService _userAppService;

        public LogoutModel(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _userAppService.Logout();

            return RedirectToPage("/Login");
        }
    }
}