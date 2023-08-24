using System.Data;
using System.Data.SqlClient;

namespace EmpCore.QueryStack.Dapper;

public class SqlConnectionFactory : IConnectionFactory
{
    private readonly ReadOnlyConnectionString _connectionString;

    public SqlConnectionFactory(ReadOnlyConnectionString connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new SqlConnection(_connectionString.Value);
        return Task.FromResult<IDbConnection>(connection);
    }
}