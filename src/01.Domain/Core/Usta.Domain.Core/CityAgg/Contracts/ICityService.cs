using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.CityAgg.Dtos;

namespace Usta.Domain.Core.CityAgg.Contracts
{
    public interface ICityService
    {
        Task<List<CityDto>> GetAllCitiesAsync(CancellationToken cancellationToken);
    }
}