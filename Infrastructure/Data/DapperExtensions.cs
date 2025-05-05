using Dapper;
using System.Data;

namespace Infrastructure.Data;

public static class DapperExtensions
{
    public static async Task<IEnumerable<T>> QueryAsync<T>(this IDbConnectionFactory factory, 
        string databaseName, string sql, object? param = null)
    {
        using IDbConnection connection = factory.CreateConnection(databaseName);
        return await connection.QueryAsync<T>(sql, param);
    }
    
    public static async Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnectionFactory factory,
        string databaseName, string sql, object? param = null)
    {
        using IDbConnection connection = factory.CreateConnection(databaseName);
        return await connection.QueryFirstOrDefaultAsync<T>(sql, param) ?? throw new InvalidOperationException();
    }

    public static async Task<int> ExecuteAsync(this IDbConnectionFactory factory,
        string databaseName, string sql, object? param = null)
    {
        using IDbConnection connection = factory.CreateConnection(databaseName);
        return await connection.ExecuteAsync(sql, param);
    }

    public static async Task<T> ExecuteScalarAsync<T>(this IDbConnectionFactory factory,
        string databaseName, string sql, object? param = null)
    {
        using IDbConnection connection = factory.CreateConnection(databaseName);
        return await connection.ExecuteScalarAsync<T>(sql, param) ?? throw new InvalidOperationException();
    }
}