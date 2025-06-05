using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;

namespace Core.Interfaces.Services;

public interface IWebMenuService
{
    // 菜单分页查询
    Task<ApiResult<PagedResult<WebMenuResult>>> GetWebMenuByPageAsync(ByWebMenuListRequest request);

    // 新增菜单
    Task<ApiResult<string>> AddWebMenuAsync(AddWebMenuRequest request);

    // 获取所有父级菜单
    Task<ApiResult<List<ParentWebMenuListResult>>> GetParentWebMenuListAsync();

    // 修改菜单
    Task<ApiResult<string>> UpdateWebMenuAsync(UpdateWebMenuRequest request);

    // 删除菜单
    Task<ApiResult<string>> DeleteWebMenuByIdAsync(string id, string companyId, string webMenuId);
    
    //  获取菜单资源列表
    Task<ApiResult<List<WebMenuResourceListResult>>> GetWebMenuResourceListAsync(ByWebMenuResourceRequest request);
}