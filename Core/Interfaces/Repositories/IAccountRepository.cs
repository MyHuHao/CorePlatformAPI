using Core.Contracts.Requests;

namespace Core.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<int> AddAsync(CreateAccountRequest request);
}