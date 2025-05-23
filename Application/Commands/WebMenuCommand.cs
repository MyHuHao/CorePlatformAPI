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
}