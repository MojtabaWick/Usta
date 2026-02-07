using Dapper;
using Usta.Domain.Core.CityAgg.Contracts;
using Usta.Domain.Core.CityAgg.Dtos;
using Usta.Domain.Core.CityAgg.Entities;
using Usta.Infrastructure.Dapper.Persistence.Contracts;

namespace Usta.Infrastructure.Dapper.Repositories.CityAgg
{
    public class CityDapperRepository : ICityRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CityDapperRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> Add(City newCity, CancellationToken cancellationToken)
        {
            const string sql = """
            INSERT INTO Cities (Name, CreatedAt)
            VALUES (@Name, @CreatedAt);
        """;

            using var connection = _connectionFactory.CreateConnection();
            var affectedRow = await connection.ExecuteAsync(
                sql,
                new
                {
                    newCity.Name,
                    CreatedAt = DateTime.Now
                });

            return affectedRow > 0;
        }

        public async Task<bool> Update(CityDto input, CancellationToken cancellationToken)
        {
            const string sql = """
            UPDATE Cities
            SET Name = @Name,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id
        """;

            using var connection = _connectionFactory.CreateConnection();
            var affectedRow = await connection.ExecuteAsync(
                sql,
                new
                {
                    input.Id,
                    input.Name,
                    UpdatedAt = DateTime.Now
                });

            return affectedRow > 0;
        }

        public async Task<CityDto?> GetById(int id, CancellationToken cancellationToken)
        {
            const string sql = """
            SELECT Id, Name
            FROM Cities
            WHERE Id = @Id
        """;

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<CityDto>(
                sql,
                new { Id = id });
        }

        public async Task<List<CityDto>> GetAllCities(CancellationToken cancellationToken)
        {
            const string sql = """
            SELECT Id, Name
            FROM Cities
        """;

            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryAsync<CityDto>(sql);
            return result.ToList();
        }
    }
}