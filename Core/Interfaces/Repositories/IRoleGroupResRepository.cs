using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IRoleGroupResRepository
{
    // 通过ID查询全部 角色组资源
    Task<IEnumerable<RoleGroupResource>> GetAllRoleGroupByIdAsync(string companyId, string roleGroupId);

    // 新增角色组资源
    Task<int> AddRoleGroupResourceAsync(RoleGroupAuthorizeRequest request);

    // 删除角色组资源
    Task<int> DeleteRoleGroupResourceAsync(RoleGroupAuthorizeRequest request);
    
    // 通过资源ID查询绑定的角色组
    Task<IEnumerable<RoleGroupResource>> GetRoleGroupByResIdAsync(string companyId, string resId);
}