using Core.Contracts;
using Core.Contracts.Requests;
using Core.DTOs;

namespace Core.Interfaces.Services;

public interface IResourceService
{
    // 分页查询-获取资源列表
    Task<ApiResult<PagedResult<ResourceDto>>> GetResourceByPageAsync(ByResourceListRequest request);

    // 添加资源
    Task<ApiResult<string>> AddResourceAsync(AddResourceRequest request);

    // 修改资源
    Task<ApiResult<string>> UpdateResourceAsync(UpdateResourceRequest request);

    // 删除资源
    Task<ApiResult<string>> DeleteResourceByIdAsync(string id);

    // 通过ID查询资源详细
    Task<ApiResult<ResourceDto>> GetResourceByIdAsync(string id);
}