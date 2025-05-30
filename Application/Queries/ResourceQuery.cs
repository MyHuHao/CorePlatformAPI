using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class ResourceQuery(IResourceRepository repository)
{
    public async Task<(IEnumerable<Resource> items, int total)> GetResourceByPageAsync(ByResourceListRequest request)
    {
        return await repository.GetResourceByPageAsync(request);
    }

    //获取资源列表
    public async Task<List<ResourceList>> GetResourceListAsync(string companyId, string webMenuId)
    {
        var result = await repository.GetResourceByPageAsync(new ByResourceListRequest()
        {
            CompanyId = companyId,
            WebMenuId = webMenuId,
            ResName = "",
            ResType = "",
            Page = 1,
            PageSize = 2000
        });
        return result.items.Select(x => new ResourceList
        {
            Id = x.Id,
            ResCode = x.ResCode,
            ResName = x.ResName
        }).ToList();
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