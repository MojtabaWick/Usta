using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Entities;

namespace Usta.Presentation.RazorPages.Areas.Account.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserAppService _userAppService;

        public LoginModel(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPost()
        {
            var loginResult = await _userAppService.LoginUserAsync(Username, Password);

            if (loginResult.Succeeded)
            {
                if (User.HasClaim(ClaimTypes.Role, "Admin"))
                {
                    return RedirectToPage("Admin/Index");
                }

                return RedirectToPage("Index");
            }
            else if (loginResult.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "حساب شما به دلیل تلاش‌های ناموفق قفل شده است.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است.");
            }

            return Page();
        }
    }
}