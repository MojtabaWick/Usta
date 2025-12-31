using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Usta.Domain.Core.CityAgg.Contracts;
using Usta.Domain.Core.CityAgg.Dtos;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Dtos;
using Usta.Presentation.RazorPages.Extentions;

namespace Usta.Presentation.RazorPages.Areas.Account.Pages
{
    [Authorize]
    public class EditModel : BasePageModel
    {
        private readonly IUserAppService _userAppService;
        private readonly ICityAppService _cityAppService;

        public EditModel(
            IUserAppService userAppService,
            ICityAppService cityAppService)
        {
            _userAppService = userAppService;
            _cityAppService = cityAppService;
        }

        [BindProperty]
        public UserEditInputDto Input { get; set; } = new();

        public List<CityDto> Cities { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            if (id != GetUserId())
                return Forbid();

            var user = await _userAppService.GetUserByIdAsync(id, cancellationToken);

            Input = new UserEditInputDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ImageUrl = user.ImageUrl,
                CityId = user.CityId
            };

            await LoadCitiesAsync(cancellationToken);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            int id,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                await LoadCitiesAsync(cancellationToken);
                return Page();
            }

            var result = await _userAppService.EditUserAsync(id, Input, cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message!);
                await LoadCitiesAsync(cancellationToken);
                return Page();
            }

            return RedirectToPage("/Profile/Index");
        }

        private async Task LoadCitiesAsync(CancellationToken cancellationToken)
        {
            Cities = await _cityAppService.GetAllCitiesAsync(cancellationToken);
        }
    }
}