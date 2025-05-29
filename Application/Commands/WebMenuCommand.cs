using Core.Contracts.Requests;
using Core.Interfaces.Repositories;

namespace Application.Commands;

public class WebMenuCommand(IWebMenuRepository repository)
{
    // 新增菜单
    public async Task AddWebMenuAsync(AddWebMenuRequest request)
    {
        await repository.AddWebMenuAsync(request);
    }

    // 修改菜单
    public async Task UpdateWebMenuAsync(UpdateWebMenuRequest request)
    {
        await repository.UpdateWebMenuAsync(request);
    }

    // 删除菜单
    public async Task DeleteWebMenuByIdAsync(string id)
    {
        await repository.DeleteWebMenuById(id);
    }
}