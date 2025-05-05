using Core.Entities;
using Core.Interfaces;
using Dapper;

namespace Infrastructure.Data.Repositories;

public class DictionaryRepository(IDbConnectionFactory connFactory) : IDictionaryRepository
{
    public async Task<IEnumerable<DictionaryItem>> GetAllAsync()
    {
        using var conn = connFactory.CreateConnection();
        return await conn.QueryAsync<DictionaryItem>("SELECT * FROM dictionary_item");
    }

    public async Task<DictionaryItem> GetByIdAsync(string id)
    {
        using var conn = connFactory.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<DictionaryItem>(
            "SELECT * FROM dictionary_item WHERE id=@Id", new { id = id }) ?? throw new InvalidOperationException();
    }

    public async Task<int> AddAsync(DictionaryItem item)
    {
        using var conn = connFactory.CreateConnection();
        return await conn.ExecuteAsync(
            "INSERT INTO dictionary_item (id,`key`, value, description) VALUES (@id,@Key, @Value, @Description)",
            item);
    }

    public async Task<int> UpdateAsync(DictionaryItem item)
    {
        using var conn = connFactory.CreateConnection();
        return await conn.ExecuteAsync(
            "UPDATE dictionary_item SET `key`=@Key, value=@Value, description=@Description WHERE id=@Id",
            item);
    }

    public async Task<int> DeleteAsync(string id)
    {
        using var conn = connFactory.CreateConnection();
        return await conn.ExecuteAsync("DELETE FROM dictionary_item WHERE id=@Id", new { id = id });
    }
}