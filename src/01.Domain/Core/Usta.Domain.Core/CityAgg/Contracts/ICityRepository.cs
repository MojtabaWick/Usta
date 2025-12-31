using Usta.Domain.Core.CityAgg.Dtos;
using Usta.Domain.Core.CityAgg.Entities;
using Usta.Domain.Core.CommentAgg.Entities;

namespace Usta.Domain.Core.CityAgg.Contracts
{
    public interface ICityRepository
    {
        public Task<bool> Add(City newCity, CancellationToken cancellationToken);

        public Task<bool> Update(CityDto input, CancellationToken cancellationToken);

        public Task<CityDto?> GetById(int id, CancellationToken cancellationToken);

        public Task<List<CityDto>> GetAllCities(CancellationToken cancellationToken);
    }
}