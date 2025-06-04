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
    
    // 修改账号数据
    public async Task UpdateAccountAsync(UpdateAccountRequest request)
    {
        await repository.UpdateAccountAsync(request);
    }
    
    // 修改密码
    public async Task UpdatePasswordAsync(VerifyPasswordRequest request)
    {
        await repository.UpdatePasswordAsync(request);
    }
}