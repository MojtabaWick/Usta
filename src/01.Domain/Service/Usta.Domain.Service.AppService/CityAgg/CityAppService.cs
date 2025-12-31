using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.CityAgg.Contracts;
using Usta.Domain.Core.CityAgg.Dtos;

namespace Usta.Domain.AppService.CityAgg
{
    public class CityAppService(ICityService cityService) : ICityAppService
    {
        public async Task<List<CityDto>> GetAllCitiesAsync(CancellationToken cancellationToken)
        {
            return await cityService.GetAllCitiesAsync(cancellationToken);
        }
    }
}