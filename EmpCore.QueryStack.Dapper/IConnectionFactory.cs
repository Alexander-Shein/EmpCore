using System.Data;

namespace EmpCore.QueryStack.Dapper;

public interface IConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}