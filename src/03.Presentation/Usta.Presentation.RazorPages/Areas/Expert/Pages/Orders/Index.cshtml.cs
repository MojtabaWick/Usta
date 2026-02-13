using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CityAgg.Contracts;
using Usta.Domain.Core.CityAgg.Dtos;
using Usta.Domain.Core.OfferAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Contracts;
using Usta.Domain.Core.OrderAgg.Dtos;
using Usta.Presentation.RazorPages.Extentions;

namespace Usta.Presentation.RazorPages.Areas.Expert.Pages.Orders
{
    public class IndexModel : BasePageModel
    {
        private readonly IOrderAppService _orderService;
        private readonly ICityAppService _cityAppService;

        public IndexModel(
            IOrderAppService orderService, ICityAppService cityAppService)
        {
            _orderService = orderService;
            _cityAppService = cityAppService;
        }

        public PagedResult<OrderDto> Orders { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CityId { get; set; }

        public List<CityDto> Cities { get; set; }

        private const int PageSize = 10;

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Cities = await _cityAppService.GetAllCitiesAsync(cancellationToken);
            Orders = await _orderService.GetOrdersForExpert((int)GetUserId()!, CityId, PageNumber, PageSize, Search, cancellationToken);
        }
    }
}