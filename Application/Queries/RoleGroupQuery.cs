using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class RoleGroupQuery(
    IRoleGroupRepository repository,
    IRoleGroupResRepository repositoryResRepository,
    IRoleGroupMenuRepository repositoryMenuRepository)
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

    // 通过ID获取所有的资源关联
    public async Task<List<RoleGroupResource>> GetAllRoleGroupResourceByIdAsync(string companyId, string roleGroupId)
    {
        var resources = await repositoryResRepository.GetAllRoleGroupByIdAsync(companyId, roleGroupId);
        return resources.Select(r => new RoleGroupResource
        {
            CompanyId = r.CompanyId,
            RoleGroupId = r.RoleGroupId,
            ResId = r.ResId,
            CreatedBy = r.CreatedBy,
            CreatedTime = r.CreatedTime,
            ModifiedBy = r.ModifiedBy,
            ModifiedTime = r.ModifiedTime
        }).ToList();
    }

    // 通过ID获取所有的菜单关联
    public async Task<List<RoleGroupWebMenu>> GetAllRoleGroupWebMenuByIdAsync(string companyId, string roleGroupId)
    {
        var result = await repositoryMenuRepository.GetAllRoleGroupByIdAsync(companyId, roleGroupId);
        return result.Select(r => new RoleGroupWebMenu
        {
            CompanyId = r.CompanyId,
            RoleGroupId = r.RoleGroupId,
            WebMenuId = r.WebMenuId,
            CreatedBy = r.CreatedBy,
            CreatedTime = r.CreatedTime,
            ModifiedBy = r.ModifiedBy,
            ModifiedTime = r.ModifiedTime
        }).ToList();
    }
}