using Core.Contracts.Requests;
using Core.Contracts.WebMenu;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IWebMenuRepository
{
    // 菜单分页查询
    Task<(IEnumerable<WebMenu> items, int total)> GetWebMenuPageAsync(ByWebMenuListRequest request);

    // 新增菜单
    Task<int> AddWebMenuAsync(AddWebMenuRequest request);

    // 修改菜单
    Task<int> UpdateWebMenuAsync(UpdateWebMenuRequest request);

    // 删除菜单
    Task<int> DeleteWebMenuById(string id);

    // 验证菜单是否合格
    Task<WebMenu?> VerifyWebMenuAsync(VerifyWebMenuRequest request);

    // 通过父菜单ID查询子菜单
    Task<IEnumerable<WebMenu>> GetChildWebMenusByParentIdAsync(string webMenuId);

    Task<IEnumerable<WebMenu>> GetWebMenuRouterListAsync();
}