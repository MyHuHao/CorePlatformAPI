using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Commands;

public class LoginCommand(
    IAccountRepository repository,
    ILoginTokenRepository loginTokenRepository,
    ILoginLogRepository loginLogRepository)
{
    /// <summary>
    /// 创建账户
    /// </summary>
    /// <param name="request"></param>
    public async Task CreateAccount(AddAccountRequest request)
    {
        await repository.AddAccountAsync(request);
    }

    // 新增登录日志
    public async Task AddLoginTokenAsync(AddLoginTokenRequest request)
    {
        await loginTokenRepository.AddLoginTokenAsync(request);
    }

    public async Task AddLoginLog(AddLoginLogRequest request)
    {
        await loginLogRepository.AddLoginLogAsync(request);
    }
}