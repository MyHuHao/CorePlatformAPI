using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IRoleGroupRepository
{
    /// <summary>
    ///     新增角色组
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<int> AddRoleGroupAsync(AddRoleGroupRequest request);

    /// <summary>
    ///     分页查询角色组
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<(IEnumerable<RoleGroup> items, int total)> GetRoleGroupPageAsync(ByRoleGroupListRequest request);

    /// <summary>
    ///     删除角色组
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<int> DeleteRoleGroupAsync(string id);

    /// <summary>
    ///     通过id查询角色组详细
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<RoleGroup?> GetRoleGroupByIdAsync(string id);

    /// <summary>
    ///     验证是否重复
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<RoleGroup?> ValidRoleGroup(ValidRoleGroupRequest request);

    /// <summary>
    ///     修改角色组
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<int> UpdateRoleGroupAsync(UpdateRoleGroupRequest request);
}