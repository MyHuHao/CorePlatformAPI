using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class ResourceQuery(IResourceRepository repository, IRoleGroupResRepository roleGroupResRepository)
{
    public async Task<(IEnumerable<Resource> items, int total)> GetResourceByPageAsync(ByResourceListRequest request)
    {
        return await repository.GetResourceByPageAsync(request);
    }

    //获取资源列表
    public async Task<List<WebMenuResourceListResult>> GetResourceListAsync(string companyId, string webMenuId)
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
        return result.items.Select(x => new WebMenuResourceListResult
        {
            Id = x.Id,
            Label = x.ResName,
            IsPenultimate = false,
            Sequence = x.ResSequence.ToString(),
            Type = "resource",
            WebMenuId = x.WebMenuId,
            Children = []
        }).OrderBy(m => m.Sequence).ToList();
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

    // 验证是否有关联角色组
    public async Task<bool> VerifyWebMenuHasRoleGroupAsync(string id, string companyId)
    {
        var result = await roleGroupResRepository.GetRoleGroupByResIdAsync(companyId, id);
        return result.Any();
    }
}