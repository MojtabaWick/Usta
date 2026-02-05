using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.CityAgg.Contracts;
using Usta.Domain.Core.CityAgg.Dtos;
using Usta.Infrastructure.Cache;
using Usta.Infrastructure.Cache.Contracts;

namespace Usta.Domain.Service.CityAgg
{
    public class CityService(ICityRepository cityRepository, ICacheService _cacheService) : ICityService
    {
        public async Task<List<CityDto>> GetAllCitiesAsync(CancellationToken cancellationToken)
        {
            var cities = _cacheService.Get<List<CityDto>?>(CacheKeys.Cities);
            if (cities is null)
            {
                cities = await cityRepository.GetAllCities(cancellationToken);
                _cacheService.SetSliding(CacheKeys.Cities, cities, 30);
            }

            return cities;
        }
    }
}