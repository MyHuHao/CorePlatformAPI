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
    /// <param name="connection"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TEntity>> QueryAsync(
        string sql,
        object? param = null,
        DbConnection? connection = null,
        DbTransaction? transaction = null)
    {
        if (connection != null && transaction != null)
        {
            var result = await connection.QueryAsync<TEntity>(sql, param, transaction);
            return result;
        }
        else
        {
            await using var conn = dbConnectionFactory.CreateConnection();
            var result = await conn.QueryAsync<TEntity>(sql, param);
            return result;
        }
    }

    /// <summary>
    ///     查询返回多数据第一条对象
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="connection"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<TEntity?> QueryFirstOrDefaultAsync(
        string sql,
        object? param = null,
        DbConnection? connection = null,
        DbTransaction? transaction = null)
    {
        if (connection != null && transaction != null)
        {
            var result = await connection.QueryFirstOrDefaultAsync<TEntity>(sql, param, transaction);
            return result;
        }
        else
        {
            await using var conn = dbConnectionFactory.CreateConnection();
            var result = await conn.QueryFirstOrDefaultAsync<TEntity>(sql, param);
            return result;
        }
    }

    /// <summary>
    ///     查询只能有一个对象数据
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="transaction"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<TEntity?> QuerySingleOrDefaultAsync(
        string sql,
        object? param = null,
        DbConnection? connection = null,
        DbTransaction? transaction = null)
    {
        if (connection != null && transaction != null)
        {
            var result = await connection.QuerySingleOrDefaultAsync<TEntity>(sql, param, transaction);
            return result;
        }
        else
        {
            await using var conn = dbConnectionFactory.CreateConnection();
            var result = await conn.QuerySingleOrDefaultAsync<TEntity>(sql, param);
            return result;
        }
    }

    /// <summary>
    ///     查询返回第一行第一列数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="connection"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public async Task<int> ExecuteScalarAsync(
        string sql,
        object? param = null,
        DbConnection? connection = null,
        DbTransaction? transaction = null)
    {
        if (connection != null && transaction != null)
        {
            var result = await connection.ExecuteScalarAsync<int>(sql, param, transaction);
            return result;
        }
        else
        {
            await using var conn = dbConnectionFactory.CreateConnection();
            var result = await conn.ExecuteScalarAsync<int>(sql, param);
            return result;
        }
    }

    /// <summary>
    ///     查询返回第一行第一列数据-字符串
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="connection"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<string> ExecuteScalarStringAsync(
        string sql,
        object? param = null,
        DbConnection? connection = null,
        DbTransaction? transaction = null)
    {
        if (connection != null && transaction != null)
        {
            var result = await connection.ExecuteScalarAsync<string>(sql, param, transaction);
            return result ?? throw new NotFoundException(MsgCodeEnum.Warning, "当前查询数据为空");
        }
        else
        {
            await using var conn = dbConnectionFactory.CreateConnection();
            var result = await conn.ExecuteScalarAsync<string>(sql, param);
            return result ?? throw new NotFoundException(MsgCodeEnum.Warning, "当前查询数据为空");
        }
    }

    /// <summary>
    ///     查询分页查询
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="connection"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<TEntity> items, int total)> QueryPageAsync(
        int page,
        int pageSize,
        string sql,
        object? param = null,
        DbConnection? connection = null,
        DbTransaction? transaction = null)
    {
        var pagedSql = $"{sql} LIMIT @Offset, @PageSize; SELECT COUNT(*) FROM ({sql}) AS totalSub";


        var parameters = new DynamicParameters(param);
        parameters.Add("Offset", (page - 1) * pageSize);
        parameters.Add("PageSize", pageSize);

        if (connection != null && transaction != null)
        {
            var multi = await connection.QueryMultipleAsync(pagedSql, parameters, transaction);
            var items = await multi.ReadAsync<TEntity>();        // 读取分页数据
            var total = await multi.ReadSingleAsync<int>(); 
            return (items,total);
        }
        else
        {
            await using var conn = dbConnectionFactory.CreateConnection();
            var multi = await conn.QueryMultipleAsync(pagedSql, parameters);
            var items = await multi.ReadAsync<TEntity>();        // 读取分页数据
            var total = await multi.ReadSingleAsync<int>(); 
            return (items,total);
        }
    }

    /// <summary>
    ///     执行sql
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="transaction"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public async Task<int> ExecuteAsync(
        string sql,
        object? param = null,
        DbConnection? connection = null,
        DbTransaction? transaction = null)
    {
        if (connection != null && transaction != null)
        {
            var result = await connection.ExecuteAsync(sql, param, transaction);
            return result;
        }
        else
        {
            await using var conn = dbConnectionFactory.CreateConnection();
            var result = await conn.ExecuteAsync(sql, param);
            return result;
        }
    }
}