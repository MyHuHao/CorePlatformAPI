using Core.Contracts.Requests;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class LoginQuery(IAccountRepository repository)
{
    public async Task<Account?> GetByIdAsync(string id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<bool> IsValidAccountPassWord(LoginRequest request)
    {
        var result = await repository.GetByIdAsync(request.Account);
        if (result == null)
        {
            return false;
        }

        // 验证密码
        var decode = HashHelper.VerifyPassword(request.PassWord, result.PasswordHash, result.PasswordSalt);
        return decode;
    }
}