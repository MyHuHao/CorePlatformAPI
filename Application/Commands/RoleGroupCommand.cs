using Core.Contracts.Requests;
using Core.Interfaces.Repositories;

namespace Application.Commands;

public class RoleGroupCommand(
    IRoleGroupRepository repository,
    IRoleGroupResRepository repositoryResRepository,
    IRoleGroupMenuRepository repositoryMenuRepository)
{
    // 新增角色组
    public async Task AddRoleGroupAsync(AddRoleGroupRequest request)
    {
        await repository.AddRoleGroupAsync(request);
    }

    // 删除
    public async Task DeleteRoleGroupAsync(string id)
    {
        await repository.DeleteRoleGroupAsync(id);
    }

    // 修改角色组
    public async Task UpdateRoleGroupAsync(UpdateRoleGroupRequest request)
    {
        await repository.UpdateRoleGroupAsync(request);
    }

    public async Task DeleteRoleGroupResourceAsync(RoleGroupAuthorizeRequest request)
    {
        await repositoryResRepository.DeleteRoleGroupResourceAsync(request);
    }

    public async Task AddRoleGroupResourceAsync(RoleGroupAuthorizeRequest request)
    {
        await repositoryResRepository.AddRoleGroupResourceAsync(request);
    }
    
    public async Task DeleteRoleGroupMenusAsync(RoleGroupAuthorizeRequest request)
    {
        await repositoryMenuRepository.DeleteRoleGroupWebMenuAsync(request);
    }
    
    public async Task AddRoleGroupMenusAsync(RoleGroupAuthorizeRequest request)
    {
        await repositoryMenuRepository.AddRoleGroupWebMenuAsync(request);
    }
}