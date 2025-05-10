using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class LoginQuery(IAccountRepository repository)
{
    public async Task<Account?> GetByIdAsync(string id)
    {
        return await repository.GetByIdAsync(id);
    }
}