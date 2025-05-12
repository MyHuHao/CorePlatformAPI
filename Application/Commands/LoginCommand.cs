using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Commands;

public class LoginCommand(IAccountRepository repository)
{
    public async Task CreateAccount(CreateAccountRequest request)
    {
        await repository.AddAsync(request);
    }

    public async Task InsertLoginToken(InsertLoginToken loginToken)
    {
        await repository.InsertLoginToken(loginToken);
    }

    public async Task InsertLogLog(InsertLoginToken loginToken)
    {
        await repository.InsertLogLog(loginToken);
    }
}