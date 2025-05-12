using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class LoginQuery(IAccountRepository repository, ILoginTokenRepository loginTokenRepository)
{
    public async Task<Account?> GetByIdAsync(string id)
    {
        return await repository.GetByIdAsync(id);
    }
    
    public async Task<LoginToken?> GetLoginTokeById(string userId)
    {
        return await loginTokenRepository.GetByIdAsync(userId);
    }
}