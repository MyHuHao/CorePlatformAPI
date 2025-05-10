using System.Data.Common;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Data;

public class DapperExtensions<TEntity>(IDbConnectionFactory dbConnectionFactory) : IDapperExtensions<TEntity>
    where TEntity : class
{
    /// <summary>
    ///     查询全部
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> QueryAsync(
        string sql,
        object? param = null,
        DbTransaction? transaction = null)
    {
        await using var conn = dbConnectionFactory.CreateConnection();
        return await conn.QueryAsync<TEntity>(sql, param, transaction);
    }

    /// <summary>
    ///     查询返回多数据第一条对象
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<TEntity> QueryFirstOrDefaultAsync(
        string sql,
        object? param = null,
        DbTransaction? transaction = null)
    {
        await using var conn = dbConnectionFactory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<TEntity>(sql, param, transaction)
               ?? throw new NotFoundException(MsgCodeEnum.Warning, "当前查询数据为空");
    }

    /// <summary>
    ///     查询只能有一个对象数据
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<TEntity> QuerySingleOrDefaultAsync(
        string sql,
        object? param = null,
        DbTransaction? transaction = null)
    {
        await using var conn = dbConnectionFactory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<TEntity>(sql, param, transaction)
               ?? throw new NotFoundException(MsgCodeEnum.Warning, "当前查询数据为空");
    }

    /// <summary>
    ///     查询返回第一行第一列数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public async Task<int> ExecuteScalarAsync(string sql, object? param = null, DbTransaction? transaction = null)
    {
        await using var conn = dbConnectionFactory.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(sql, param, transaction);
    }

    /// <summary>
    ///     查询返回第一行第一列数据-字符串
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<string> ExecuteScalarStringAsync(
        string sql,
        object? param = null,
        DbTransaction? transaction = null)
    {
        await using var conn = dbConnectionFactory.CreateConnection();
        return await conn.ExecuteScalarAsync<string>(sql, param, transaction)
               ?? throw new NotFoundException(MsgCodeEnum.Warning, "当前查询数据为空");
    }

    /// <summary>
    ///     查询分页查询
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<TEntity> items, int total)> QueryPageAsync(
        int page,
        int pageSize,
        string sql,
        object? param = null,
        DbTransaction? transaction = null)
    {
        await using var conn = dbConnectionFactory.CreateConnection();
        // 在MySQL中通过 LIMIT 分页并获取总数
        var pagedSql = $"{sql} LIMIT @Offset, @PageSize; SELECT COUNT(*) FROM ({sql}) AS totalSub";
        var multi = await conn.QueryMultipleAsync(pagedSql,
            new { Offset = (page - 1) * pageSize, PageSize = pageSize, param }, transaction);
        var items = await multi.ReadAsync<TEntity>();
        var total = await multi.ReadSingleAsync<int>();
        return (items, total);
    }

    /// <summary>
    ///     执行sql
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public async Task<int> ExecuteAsync(string sql, object? param = null, DbTransaction? transaction = null)
    {
        await using var conn = dbConnectionFactory.CreateConnection();
        return await conn.ExecuteAsync(sql, param, transaction);
    }
}