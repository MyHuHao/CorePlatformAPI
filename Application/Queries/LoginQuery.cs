using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class LoginQuery(IAccountRepository repository, ILoginTokenRepository loginTokenRepository)
{
    /// <summary>
    /// 通过账号查询账号全部信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Account?> GetAccountById(ByAccountRequest request)
    {
        return await repository.GetByAccountAsync(request);
    }

    /// <summary>
    /// 验证token是否合格
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<bool> VerifyLoginTokenAsync(ByLoginTokenRequest request)
    {
        return await loginTokenRepository.VerifyLoginTokenAsync(request);
    }
}