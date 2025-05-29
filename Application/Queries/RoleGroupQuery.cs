using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class RoleGroupQuery(IRoleGroupRepository repository)
{
    /// <summary>
    ///     通过id查询角色组详细
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<RoleGroup?> GetRoleGroupByIdAsync(string id)
    {
        return await repository.GetRoleGroupByIdAsync(id);
    }

    /// <summary>
    ///     验证是否有该角色组
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<bool> ValidRoleGroup(ValidRoleGroupRequest request)
    {
        return await repository.ValidRoleGroup(request) != null;
    }

    public async Task<(IEnumerable<RoleGroup> items, int total)> GetRoleGroupPageAsync(ByRoleGroupListRequest request)
    {
        return await repository.GetRoleGroupPageAsync(request);
    }
}