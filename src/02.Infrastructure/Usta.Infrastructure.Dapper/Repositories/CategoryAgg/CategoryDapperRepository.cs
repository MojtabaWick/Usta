using Dapper;
using Usta.Domain.Core.CategoryAgg.Contracts;
using Usta.Domain.Core.CategoryAgg.Dtos;
using Usta.Infrastructure.Dapper.Persistence.Contracts;

namespace Usta.Infrastructure.Dapper.Repositories.CategoryAgg
{
    public class CategoryDapperRepository : ICategoryDapperRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CategoryDapperRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
        {
            const string sql = """
                                   SELECT Id, Title, Description, ImagedUrl
                                   FROM Categories
                                   WHERE IsDeleted = 0
                               """;

            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryAsync<CategoryDto>(sql);
            return result.ToList();
        }

        public async Task<List<CategorySelectDto>> GetCategoriesForSelect(CancellationToken cancellationToken)
        {
            const string sql = """
                                   SELECT Id, Title
                                   FROM Categories
                                   WHERE IsDeleted = 0
                               """;

            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryAsync<CategorySelectDto>(sql);
            return result.ToList();
        }
    }
}