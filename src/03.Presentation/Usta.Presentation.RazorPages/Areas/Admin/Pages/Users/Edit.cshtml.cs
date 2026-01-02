using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.CityAgg.Contracts;
using Usta.Domain.Core.CityAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Domain.Core.UserAgg.Contracts;
using Usta.Domain.Core.UserAgg.Dtos;

namespace Usta.Presentation.RazorPages.Areas.Admin.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly IUserAppService _userAppService;
        private readonly IProvidedServiceAppService _serviceAppService;
        private readonly ICityAppService _cityAppService;

        public EditModel(
            IUserAppService userAppService,
            IProvidedServiceAppService serviceAppService,
            ICityAppService cityAppService)
        {
            _userAppService = userAppService;
            _serviceAppService = serviceAppService;
            _cityAppService = cityAppService;
        }

        [BindProperty]
        public AdminUserEditDto Input { get; set; } = null!;

        public List<ProfileProvidedServiceDto> Services { get; set; } = [];
        public List<CityDto> Cities { get; set; } = [];

        // GET
        public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
        {
            Input = await _userAppService.GetUserForAdminEditAsync(id, ct);

            Cities = await _cityAppService.GetAllCitiesAsync(ct);

            if (Input.Role == "Expert")
            {
                Services = await _serviceAppService.GetAllForProfileAsync(ct);
            }

            return Page();
        }

        // POST
        public async Task<IActionResult> OnPostAsync(int id, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                Cities = await _cityAppService.GetAllCitiesAsync(ct);

                if (Input.Role == "Expert")
                {
                    Services = await _serviceAppService.GetAllForProfileAsync(ct);
                }

                return Page();
            }

            var result = await _userAppService.AdminEditUserAsync(id, Input, ct);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message!);

                Cities = await _cityAppService.GetAllCitiesAsync(ct);

                if (Input.Role == "Expert")
                {
                    Services = await _serviceAppService.GetAllForProfileAsync(ct);
                }

                return Page();
            }

            return RedirectToPage("/Users/Index", new { area = "Admin" });
        }
    }
}