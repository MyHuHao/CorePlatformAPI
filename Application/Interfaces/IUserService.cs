using Core.DTOs;
using Core.DTOs.Base;
using Core.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<ApiResponse<User>> GetUserByIdAsync(string id);
    Task<IEnumerable<ApiResponse<PagedResult<User>>>> GetAllUsersAsync(GetAllUserRequest request);
    Task<int> CreateUserAsync(GetAllUserRequest request);
    Task<bool> DeleteUserAsync(string id);
}