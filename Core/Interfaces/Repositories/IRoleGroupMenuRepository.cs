using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IRoleGroupMenuRepository
{
    // 通过ID查询全部菜单数据
    Task<IEnumerable<RoleGroupWebMenu>> GetAllRoleGroupByIdAsync(string companyId, string roleGroupId);

    // 添加角色组菜单
    Task<int> AddRoleGroupWebMenuAsync(RoleGroupAuthorizeRequest request);

    // 删除角色组菜单
    Task<int> DeleteRoleGroupWebMenuAsync(RoleGroupAuthorizeRequest request);
    
    // 通过菜单ID查询绑定的角色组
    Task<IEnumerable<RoleGroupWebMenu>> GetRoleGroupByMenuIdAsync(string companyId, string webMenuId);
}