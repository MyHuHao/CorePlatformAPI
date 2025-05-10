using Core.Contracts.Requests;
using Core.Interfaces.Repositories;

namespace Application.Commands;

public class LoginCommand(IAccountRepository repository)
{
    public async Task CreateAccount(CreateAccountRequest request)
    {
        await repository.AddAsync(request);
    }
}