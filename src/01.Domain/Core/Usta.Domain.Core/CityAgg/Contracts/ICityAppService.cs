using System;
using System.Collections.Generic;
using System.Text;
using Usta.Domain.Core.CityAgg.Dtos;

namespace Usta.Domain.Core.CityAgg.Contracts
{
    public interface ICityAppService
    {
        Task<List<CityDto>> GetAllCitiesAsync(CancellationToken cancellationToken);
    }
}