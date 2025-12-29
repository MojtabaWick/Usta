using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Dtos;

namespace Usta.Presentation.RazorPages.Areas.Account.Pages
{
    public class RegisterModel(IUserAppService userAppService) : PageModel
    {
        [BindProperty]
        public UserRegisterInputDto Input { get; set; }

        public void OnGet()
        {
            Input = new UserRegisterInputDto();
        }

        public async Task<IActionResult> OnPost(CancellationToken canCancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await userAppService.RegisterUserAsync(Input, canCancellationToken);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}