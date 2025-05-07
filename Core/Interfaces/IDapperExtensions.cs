namespace Core.Interfaces;

public interface IDapperExtensions<TEntity> where TEntity : class
{
    /// <summary>
    /// 查询全部
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> QueryAsync(string sql, object? param = null);

    /// <summary>
    /// 返回多数据第一条对象
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    Task<TEntity> QueryFirstOrDefaultAsync(string sql, object? param = null);

    /// <summary>
    /// 只能有一个对象数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    Task<TEntity> QuerySingleOrDefaultAsync(string sql, object? param = null);
    
    /// <summary>
    /// 返回第一行第一列数据
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    Task<int> ExecuteScalarAsync(string sql, object? param = null);

    /// <summary>
    /// 查询返回第一行第一列数据-字符串
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    Task<string> ExecuteScalarStringAsync(string sql, object? param = null);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    Task<(IEnumerable<TEntity> items, int total)> QueryPageAsync(int page, int pageSize, string sql,
        object? param = null);

    /// <summary>
    /// 执行sql
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    Task<int> ExecuteAsync(string sql, object? param = null);
}