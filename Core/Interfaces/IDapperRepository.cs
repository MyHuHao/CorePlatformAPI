namespace Core.Interfaces;

public interface IDapperRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> QueryAsync(string sql, object? param = null);

    Task<TEntity> QueryFirstOrDefaultAsync(string sql, object? param = null);

    Task<(IEnumerable<TEntity> items, int total)> QueryPageAsync(int page, int pageSize, string sql,
        object? param = null);

    Task<int> ExecuteAsync(string sql, object? param = null);

    // 其他通用方法签名...
}