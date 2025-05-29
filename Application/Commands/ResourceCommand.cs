using Core.Contracts.Requests;
using Core.Interfaces.Repositories;

namespace Application.Commands;

public class ResourceCommand(IResourceRepository repository)
{
    /// <summary>
    /// 新增数据
    /// </summary>
    /// <param name="request"></param>
    public async Task AddResourceAsync(AddResourceRequest request)
    {
        await repository.AddResourceAsync(request);
    }
    
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <param name="request"></param>
    public async Task UpdateResourceAsync(UpdateResourceRequest request)
    {
        await repository.UpdateResourceAsync(request);
    }
    
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="id"></param>
    public async Task DeleteResourceByIdAsync(string id)
    {
        await repository.DeleteResourceByIdAsync(id);
    }
}