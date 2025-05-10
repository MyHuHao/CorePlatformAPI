using Core.Contracts.Requests;

namespace Application.Commands;

public class LoginCommand(IAccountRepository repository)
{
    public async Task CreateAccount(CreateAccountRequest request)
    {
        await repository.AddAsync(request);
    }
}