using System.Data;

namespace Usta.Infrastructure.Dapper.Persistence.Contracts
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}