using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;
using Core.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<ApiResults<UserDto>> GetUserByIdAsync(string id);
    Task<IEnumerable<ApiResults<PagedResult<User>>>> GetAllUsersAsync(GetAllUserRequest request);
    Task<int> CreateUserAsync(GetAllUserRequest request);
    Task<bool> DeleteUserAsync(string id);
}