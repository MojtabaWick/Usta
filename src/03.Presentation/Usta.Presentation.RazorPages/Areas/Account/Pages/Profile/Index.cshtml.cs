using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Dtos;
using Usta.Presentation.RazorPages.Extentions;

namespace Usta.Presentation.RazorPages.Areas.Account.Pages.Profile
{
    [Authorize]
    public class IndexModel(IUserAppService userAppService) : BasePageModel
    {
        public UserDto User { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            int userId = (int)GetUserId()!;

            if (IsExpert())
            {
                User = await userAppService
                    .GetExpertUserWithServicesAsync(userId, cancellationToken);
            }
            else
            {
                User = await userAppService
                    .GetUserByIdAsync(userId, cancellationToken);
            }

            return Page();
        }
    }
}