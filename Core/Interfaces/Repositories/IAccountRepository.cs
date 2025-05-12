using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(string id);
    Task<int> AddAsync(CreateAccountRequest request);
    Task<int> InsertLoginToken(InsertLoginToken loginToken);
    Task<int> InsertLogLog(InsertLoginToken loginToken);
}