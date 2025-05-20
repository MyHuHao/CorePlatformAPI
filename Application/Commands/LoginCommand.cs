using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Commands;

public class LoginCommand(IAccountRepository repository)
{
    /// <summary>
    /// 创建账户
    /// </summary>
    /// <param name="request"></param>
    public async Task CreateAccount(AddAccountRequest request)
    {
        await repository.AddAccountAsync(request);
    }
}