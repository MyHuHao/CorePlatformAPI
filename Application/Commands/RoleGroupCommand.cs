using Core.Contracts.Requests;
using Core.Interfaces.Repositories;

namespace Application.Commands;

public class RoleGroupCommand(IRoleGroupRepository repository)
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
}