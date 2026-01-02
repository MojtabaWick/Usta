using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Dtos;
using Usta.Domain.Core.UserAgg.Enums;

namespace Usta.Presentation.RazorPages.Areas.Admin.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserAppService _userAppService;

        public IndexModel(IUserAppService service)
        {
            _userAppService = service;
        }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public UserType? UserType { get; set; }

        public PagedResult<UserDto> Users { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Users = await _userAppService.GetAllUsersAsync(PageNumber, 10, Search, UserType, cancellationToken);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _userAppService.DeleteAsync(id, cancellationToken);
            return RedirectToPage("Index");
        }
    }
}