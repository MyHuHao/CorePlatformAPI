using Core.Contracts.Requests;
using Core.Interfaces.Repositories;

namespace Application.Commands;

public class AccountCommand(IAccountRepository repository)
{
    // 新增账号
    public async Task AddAccountAsync(AddAccountRequest request)
    {
        await repository.AddAccountAsync(request);
    }
    
    // 删除账号
    public async Task DeleteAccountAsync(string id)
    {
        await repository.DeleteAccountAsync(id);
    }
}