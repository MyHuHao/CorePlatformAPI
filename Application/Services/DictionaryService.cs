using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public class DictionaryService(IDictionaryRepository repo)
{
    public Task<IEnumerable<DictionaryItem>> GetListAsync() => repo.GetAllAsync();
    public Task<DictionaryItem> GetAsync(string id) => repo.GetByIdAsync(id);
    public Task<int> CreateAsync(DictionaryItem item) => repo.AddAsync(item);
    public Task<int> UpdateAsync(DictionaryItem item) => repo.UpdateAsync(item);
    public Task<int> DeleteAsync(string id) => repo.DeleteAsync(id);
}