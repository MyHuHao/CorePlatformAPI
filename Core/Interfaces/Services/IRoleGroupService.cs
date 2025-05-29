using Core.Contracts;
using Core.Contracts.Requests;
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
}