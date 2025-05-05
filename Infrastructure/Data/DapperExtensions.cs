using Dapper;
using System.Data;

namespace Infrastructure.Data;

public interface IDapperRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> QueryAsync(string sql, object? param = null);

    Task<TEntity> QueryFirstOrDefaultAsync(string sql, object? param = null);

   Task<(IEnumerable<TEntity> items, int total)> QueryPageAsync(int page, int pageSize, string sql, object? param = null);
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

    public async Task<TEntity> QueryFirstOrDefaultAsync(string sql, object? param = null)
    {
        using var conn = dbConnectionFactory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TEntity>(sql, param) ?? throw new InvalidOperationException();
    }
    
    public async Task<(IEnumerable<TEntity> items, int total)> QueryPageAsync(int page, int pageSize, string sql, object param = null)
    {
        using var conn = dbConnectionFactory.CreateConnection();
        // 在MySQL中通过 LIMIT 分页并获取总数
        var pagedSql = $"{sql} LIMIT @Offset, @PageSize; SELECT COUNT(*) FROM ({sql}) AS totalSub";
        var multi = await conn.QueryMultipleAsync(pagedSql, new { Offset = (page - 1) * pageSize, PageSize = pageSize, param });
        var items = await multi.ReadAsync<TEntity>();
        var total = await multi.ReadSingleAsync<int>();
        return (items, total);
    }

    public async Task<int> ExecuteAsync(string sql, object? param = null)
    {
        using var conn = dbConnectionFactory.CreateConnection();
        return await conn.ExecuteAsync(sql, param);
    }
}