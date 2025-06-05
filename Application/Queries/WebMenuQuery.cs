using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class WebMenuQuery(IWebMenuRepository repository, IRoleGroupMenuRepository roleGroupMenuRepository)
{
    // 菜单分页查询
    public async Task<(IEnumerable<WebMenu> items, int total)> GetWebMenuPageAsync(ByWebMenuListRequest request)
    {
        return await repository.GetWebMenuPageAsync(request);
    }

    // 验证菜单
    public async Task<bool> VerifyWebMenuAsync(VerifyWebMenuRequest request)
    {
        return await repository.VerifyWebMenuAsync(request) != null;
    }

    // 验证该菜单是否有子菜单
    public async Task<bool> VerifyWebMenuHasChildAsync(string webMenuId)
    {
        var result = await repository.GetChildWebMenusByParentIdAsync(webMenuId);
        return result.Any();
    }

    // 验证该菜单是否有关联角色组
    public async Task<bool> VerifyWebMenuHasRoleGroupAsync(string companyId, string webMenuId)
    {
        var result = await roleGroupMenuRepository.GetRoleGroupByMenuIdAsync(companyId, webMenuId);
        return result.Any();
    }
}