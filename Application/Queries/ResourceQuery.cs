using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class ResourceQuery(IResourceRepository repository)
{
    public async Task<(IEnumerable<Resource> items, int total)> GetResourceByPageAsync(ByResourceListRequest request)
    {
        return await repository.GetResourceByPageAsync(request);
    }
    
    public async Task<bool> ValidResourceAsync(ValidResourceCodeRequest request)
    {
        return await repository.ValidResourceAsync(request) != null;
    }
    
    // 通过ID查询资源详细
    public async Task<Resource?> GetResourceByIdAsync(string id)
    {
        return await repository.GetResourceByIdAsync(id);
    }
}