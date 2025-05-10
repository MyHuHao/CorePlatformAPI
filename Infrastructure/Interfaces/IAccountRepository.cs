using Core.Contracts.Requests;

namespace Infrastructure.Interfaces;

public interface IAccountRepository
{
    Task<int> AddAsync(CreateAccountRequest request);
}