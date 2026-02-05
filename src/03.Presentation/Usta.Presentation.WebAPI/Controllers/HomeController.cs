using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CategoryAgg.Contracts;
using Usta.Domain.Core.CategoryAgg.Dtos;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;

namespace Usta.Presentation.WebAPI.Controllers
{
    // ReSharper disable once HollowTypeName
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(ICategoryAppService categoryAppService, IProvidedServiceAppService pSAppService) : ControllerBase
    {
        [HttpGet(nameof(Categories))]
        public async Task<List<CategoryDto>> Categories(CancellationToken cancellationToken)
        {
            return await categoryAppService.GetAllCategories(cancellationToken);
        }

        [HttpGet(nameof(ProvidedServices))]
        public async Task<PagedResult<ProvidedServiceDto>> ProvidedServices(int pageNumber, string? search, CancellationToken cancellationToken)
        {
            return await pSAppService.GetAllForHome(pageNumber, 10, search, cancellationToken);
        }
    }
}