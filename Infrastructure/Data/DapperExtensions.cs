using Core.Interfaces;
using Dapper;

namespace Infrastructure.Data;

public class DapperExtensions<TEntity>(IDbConnectionFactory dbConnectionFactory) : IDapperExtensions<TEntity>
    where TEntity : class
{
    /// <summary>
    /// 查询全部
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> QueryAsync(string sql, object? param = null)
    {
        using var conn = dbConnectionFactory.CreateConnection();
        return await conn.QueryAsync<TEntity>(sql, param);
    }

    /// <summary>
    /// 查询返回多数据第一条对象
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<TEntity> QueryFirstOrDefaultAsync(string sql, object? param = null)
    {
        using var conn = dbConnectionFactory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TEntity>(sql, param) ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// 查询只能有一个对象数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<TEntity> QuerySingleOrDefaultAsync(string sql, object? param = null)
    {
        using var conn = dbConnectionFactory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<TEntity>(sql, param) ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// 查询返回第一行第一列数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<int> ExecuteScalarAsync(string sql, object? param = null)
    {
        using var conn = dbConnectionFactory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(sql, param);
    }

    /// <summary>
    /// 查询返回第一行第一列数据-字符串
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<string> ExecuteScalarStringAsync(string sql, object? param = null)
    {
        using var conn = dbConnectionFactory.CreateConnection();
        return await conn.ExecuteScalarAsync<string>(sql, param) ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// 查询分页查询
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<TEntity> items, int total)> QueryPageAsync(int page, int pageSize, string sql,
        object? param = null)
    {
        using var conn = dbConnectionFactory.CreateConnection();
        // 在MySQL中通过 LIMIT 分页并获取总数
        var pagedSql = $"{sql} LIMIT @Offset, @PageSize; SELECT COUNT(*) FROM ({sql}) AS totalSub";
        var multi = await conn.QueryMultipleAsync(pagedSql,
            new { Offset = (page - 1) * pageSize, PageSize = pageSize, param });
        var items = await multi.ReadAsync<TEntity>();
        var total = await multi.ReadSingleAsync<int>();
        return (items, total);
    }

    /// <summary>
    /// 执行sql
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public async Task<int> ExecuteAsync(string sql, object? param = null)
    {
        using var conn = dbConnectionFactory.CreateConnection();
        return await conn.ExecuteAsync(sql, param);
    }
}