using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;

namespace Core.Interfaces.Services;

public interface IRoleGroupService
{
    // 新增角色组
    Task<ApiResult<string>> AddRoleGroupAsync(AddRoleGroupRequest request);

    // 分页查询角色组
    Task<ApiResult<PagedResult<RoleGroupDto>>> GetRoleGroupPageAsync(ByRoleGroupListRequest request);

    // 删除角色组
    Task<ApiResult<string>> DeleteRoleGroupAsync(string id);

    // 通过id查询角色组详细
    Task<ApiResult<RoleGroupDto>> GetRoleGroupByIdAsync(string id);

    // 修改角色组
    Task<ApiResult<string>> UpdateRoleGroupAsync(UpdateRoleGroupRequest request);

    // 角色组授权菜单和资源
    Task<ApiResult<string>> RoleGroupAuthorizeAsync(RoleGroupAuthorizeRequest request);
    
    // 通过ID获取已经授权的菜单和资源
    Task<ApiResult<List<string>>> GetRoleGroupAuthorizeByIdAsync(ByRoleGroupRequest request);
}