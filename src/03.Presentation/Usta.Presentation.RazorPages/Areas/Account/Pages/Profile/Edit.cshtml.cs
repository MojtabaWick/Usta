using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Usta.Domain.Core.CityAgg.Contracts;
using Usta.Domain.Core.CityAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Dtos;
using Usta.Presentation.RazorPages.Extentions;

namespace Usta.Presentation.RazorPages.Areas.Account.Pages.Profile
{
    [Authorize]
    public class EditModel : BasePageModel
    {
        private readonly IUserAppService _userAppService;
        private readonly ICityAppService _cityAppService;
        private readonly IProvidedServiceAppService _serviceAppService;

        public EditModel(
            IUserAppService userAppService,
            ICityAppService cityAppService,
            IProvidedServiceAppService providedServiceApp)
        {
            _userAppService = userAppService;
            _cityAppService = cityAppService;
            _serviceAppService = providedServiceApp;
        }

        [BindProperty]
        public UserEditInputDto Input { get; set; } = new();

        public List<ProfileProvidedServiceDto> Services { get; set; } = [];
        public List<CityDto> Cities { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            if (id != GetUserId())
                return Forbid();

            if (IsExpert())
            {
                await LoadServicesAsync(cancellationToken);

                var user = await _userAppService.GetExpertUserWithServicesAsync(id, cancellationToken);
                Input = new UserEditInputDto
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    ImageUrl = user.ImageUrl,
                    CityId = user.CityId,
                    ServiceIds = user.Services.Select(s => s.Id).ToList()
                };
            }
            else
            {
                var user = await _userAppService.GetUserByIdAsync(id, cancellationToken);
                Input = new UserEditInputDto
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    ImageUrl = user.ImageUrl,
                    CityId = user.CityId,
                };
            }
            await LoadCitiesAsync(cancellationToken);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            int id,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                if (IsExpert())
                {
                    await LoadServicesAsync(cancellationToken);
                }

                await LoadCitiesAsync(cancellationToken);
                return Page();
            }

            var result = await _userAppService.EditUserAsync(id, Input, cancellationToken);

            if (IsExpert())
            {
                var servicesUpdate = await _userAppService.UpdateExpertServices(id, Input.ServiceIds, cancellationToken);
                if (!servicesUpdate)
                {
                    ModelState.AddModelError("", "آپدیت سرویس های کارشناس با مشکل مواجه شده است.");
                }
            }

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message!);

                if (IsExpert())
                {
                    await LoadServicesAsync(cancellationToken);
                }

                await LoadCitiesAsync(cancellationToken);
                return Page();
            }

            return RedirectToPage("/Profile/Index");
        }

        private async Task LoadCitiesAsync(CancellationToken cancellationToken)
        {
            Cities = await _cityAppService.GetAllCitiesAsync(cancellationToken);
        }

        private async Task LoadServicesAsync(CancellationToken cancellationToken)
        {
            Services = await _serviceAppService.GetAllForProfileAsync(cancellationToken);
        }
    }
}