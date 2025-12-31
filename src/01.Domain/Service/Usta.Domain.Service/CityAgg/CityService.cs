using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.CityAgg.Contracts;
using Usta.Domain.Core.CityAgg.Dtos;

namespace Usta.Domain.Service.CityAgg
{
    public class CityService(ICityRepository cityRepository) : ICityService
    {
        public async Task<List<CityDto>> GetAllCitiesAsync(CancellationToken cancellationToken)
        {
            return await cityRepository.GetAllCities(cancellationToken);
        }
    }
}