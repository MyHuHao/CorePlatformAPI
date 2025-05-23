using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IWebMenuRepository
{
    // 菜单分页查询
    Task<(IEnumerable<WebMenu> items, int total)> GetWebMenuPageAsync(ByWebMenuListRequest request);
    
    // 新增菜单
    Task<int> AddWebMenuAsync(AddWebMenuRequest request);
}