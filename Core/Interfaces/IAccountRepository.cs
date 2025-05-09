using Core.Contracts.Requests;

namespace Core.Interfaces;

public interface IAccountRepository
{
    Task<int> AddAsync(CreateAccountRequest request);
}