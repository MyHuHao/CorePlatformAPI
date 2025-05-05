using Core.Entities;

namespace Core.Interfaces;

public interface IDictionaryRepository
{
    Task<IEnumerable<DictionaryItem>> GetAllAsync();
    Task<DictionaryItem> GetByIdAsync(string id);
    Task<int> AddAsync(DictionaryItem item);
    Task<int> UpdateAsync(DictionaryItem item);
    Task<int> DeleteAsync(string id);
}