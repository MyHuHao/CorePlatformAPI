using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class WebMenuQuery(IWebMenuRepository repository)
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
}