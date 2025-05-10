using Core.Contracts.Requests;
using Core.Entities;

namespace Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(string id);
    Task<IEnumerable<User>> GetAllAsync(GetAllUserRequest request);
    Task<string> AddAsync(User user);
    Task<bool> DeleteAsync(string id);
}