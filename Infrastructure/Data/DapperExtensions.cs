using Dapper;
using System.Data;

namespace Infrastructure.Data;

public interface IDapperRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> QueryAsync(string sql, object? param = null);

    Task<int> ExecuteAsync(string sql, object? param = null);

    // 其他通用方法签名...
}

public class DapperRepository<TEntity>(IDbConnectionFactory dbConnectionFactory) : IDapperRepository<TEntity>
    where TEntity : class
{
    public async Task<IEnumerable<TEntity>> QueryAsync(string sql, object? param = null)
    {
        using var conn = dbConnectionFactory.CreateConnection();
        return await conn.QueryAsync<TEntity>(sql, param);
    }

    public async Task<int> ExecuteAsync(string sql, object? param = null)
    {
        using var conn = dbConnectionFactory.CreateConnection();
        return await conn.ExecuteAsync(sql, param);
    }
}