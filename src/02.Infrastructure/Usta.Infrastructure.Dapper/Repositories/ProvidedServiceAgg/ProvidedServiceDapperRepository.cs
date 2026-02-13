using Dapper;
using Usta.Domain.Core._common;
using Usta.Domain.Core.ProvidedServiceAgg.Contracts;
using Usta.Domain.Core.ProvidedServiceAgg.Dtos;
using Usta.Infrastructure.Dapper.Persistence.Contracts;

namespace Usta.Infrastructure.Dapper.Repositories.ProvidedServiceAgg
{
    public class ProvidedServiceDapperRepository : IProvidedServiceDapperRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ProvidedServiceDapperRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<ProfileProvidedServiceDto>> GetAllForProfileAsync(CancellationToken cancellationToken)
        {
            const string sql = """
                                   SELECT p.Id, p.Title
                                   FROM ProvidedServices p
                                   INNER JOIN Categories c ON p.CategoryId = c.Id
                                   WHERE p.IsDeleted = 0
                                     AND c.IsDeleted = 0
                               """;

            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryAsync<ProfileProvidedServiceDto>(sql);
            return result.ToList();
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllProvidedService(
            int pageNumber,
            int pageSize,
            string? search,
            CancellationToken cancellationToken)
        {
            using var connection = _connectionFactory.CreateConnection();

            var parameters = new DynamicParameters();

            var sql = """
                          SELECT
                              p.Id,
                              p.Title,
                              p.Description,
                              p.ImageUrl,
                              p.MinPrice,
                              c.Title as CategoryName
                          FROM ProvidedServices p
                          INNER JOIN Categories c ON p.CategoryId = c.Id
                          WHERE p.IsDeleted = 0
                            AND c.IsDeleted = 0
                      """;

            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += """
                           AND (
                               p.Title LIKE @Search OR
                               (p.Description IS NOT NULL AND p.Description LIKE @Search) OR
                               c.Title LIKE @Search
                           )
                       """;
                parameters.Add("@Search", $"%{search}%");
            }

            var countSql = $"SELECT COUNT(*) FROM ({sql}) as CountQuery";
            var totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters);

            var pagedSql = sql + """
                                     ORDER BY p.Id
                                     OFFSET @Offset ROWS
                                     FETCH NEXT @PageSize ROWS ONLY
                                 """;

            var pagedParameters = new DynamicParameters(parameters);
            pagedParameters.Add("@Offset", (pageNumber - 1) * pageSize);
            pagedParameters.Add("@PageSize", pageSize);

            var items = await connection.QueryAsync<ProvidedServiceDto>(pagedSql, pagedParameters);

            return new PagedResult<ProvidedServiceDto>
            {
                Items = items.ToList(),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllProvidedServiceForAdmin(
            int pageNumber,
            int pageSize,
            string? search,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            var sql = """
            SELECT
                p.Id,
                p.Title,
                p.Description,
                p.ImageUrl,
                p.MinPrice,
                c.Title as CategoryName
            FROM ProvidedServices p
            INNER JOIN Categories c ON p.CategoryId = c.Id
            WHERE p.IsDeleted = 0
        """;

            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += """
                AND (
                    p.Title LIKE @Search OR
                    (p.Description IS NOT NULL AND p.Description LIKE @Search) OR
                    c.Title LIKE @Search
                )
            """;
                parameters.Add("@Search", $"%{search}%");
            }

            var countSql = $"SELECT COUNT(*) FROM ({sql}) as CountQuery";

            using var connection = _connectionFactory.CreateConnection();
            var totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters);

            sql += """
            ORDER BY p.Id
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY
        """;

            parameters.Add("@Offset", (pageNumber - 1) * pageSize);
            parameters.Add("@PageSize", pageSize);

            var items = await connection.QueryAsync<ProvidedServiceDto>(sql, parameters);

            return new PagedResult<ProvidedServiceDto>
            {
                Items = items.ToList(),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PagedResult<ProvidedServiceDto>> GetAllProvidedServiceByCategory(
            int categoryId,
            int pageNumber,
            int pageSize,
            string? search,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryId", categoryId);

            var sql = """
                          SELECT
                              p.Id,
                              p.Title,
                              p.Description,
                              p.ImageUrl,
                              p.MinPrice,
                              c.Title as CategoryName
                          FROM ProvidedServices p
                          INNER JOIN Categories c ON p.CategoryId = c.Id
                          WHERE p.CategoryId = @CategoryId
                            AND p.IsDeleted = 0
                            AND c.IsDeleted = 0
                      """;

            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += """
                AND (
                    p.Title LIKE @Search OR
                    (p.Description IS NOT NULL AND p.Description LIKE @Search) OR
                    c.Title LIKE @Search
                )
            """;
                parameters.Add("@Search", $"%{search}%");
            }

            var countSql = $"SELECT COUNT(*) FROM ({sql}) as CountQuery";

            using var connection = _connectionFactory.CreateConnection();
            var totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters);

            sql += """
            ORDER BY p.Id
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY
        """;

            parameters.Add("@Offset", (pageNumber - 1) * pageSize);
            parameters.Add("@PageSize", pageSize);

            var items = await connection.QueryAsync<ProvidedServiceDto>(sql, parameters);

            return new PagedResult<ProvidedServiceDto>
            {
                Items = items.ToList(),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}