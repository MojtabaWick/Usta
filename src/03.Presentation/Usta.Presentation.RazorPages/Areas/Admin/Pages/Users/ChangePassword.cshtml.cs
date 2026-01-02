using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Presentation.RazorPages.Extentions;

namespace Usta.Presentation.RazorPages.Areas.Admin.Pages.Users
{
    [Authorize]
    public class ChangePasswordModel : BasePageModel
    {
        private readonly IUserAppService _userAppService;

        public ChangePasswordModel(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [BindProperty]
        [Required(ErrorMessage = "پسورد جدید الزامی است.")]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد جدید")]
        public string NewPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "تکرار پسورد جدید الزامی است.")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار پسورد جدید")]
        [Compare("NewPassword", ErrorMessage = "پسورد جدید و تکرار آن مطابقت ندارند.")]
        public string ConfirmPassword { get; set; }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userAppService.ChangePasswordWithAdmin(id, NewPassword);

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
                return Page();
            }
        }
    }
}