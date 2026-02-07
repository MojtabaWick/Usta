using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Usta.Infrastructure.Dapper.Persistence.Contracts;

namespace Usta.Infrastructure.Dapper.Persistence.SqlServer
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQL")!;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}