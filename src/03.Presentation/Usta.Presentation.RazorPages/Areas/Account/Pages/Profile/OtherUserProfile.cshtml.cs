using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Dtos;

namespace Usta.Presentation.RazorPages.Areas.Account.Pages.Profile
{
    public class OtherUserProfileModel : PageModel
    {
        private readonly IUserAppService _userAppService;

        public OtherUserProfileModel(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public ExpertProfileSummeryDto Expert { get; set; }

        [BindProperty(SupportsGet = true)]
        public int UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            Expert = await _userAppService.GetExpertSummeryProfile(UserId, cancellationToken);
            return Page();
        }
    }
}