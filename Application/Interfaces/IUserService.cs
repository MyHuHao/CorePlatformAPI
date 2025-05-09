using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;
using Core.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    /// <summary>
    /// 通过用户ID获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ApiResults<UserDto>> GetUserByIdAsync(string id);
    Task<IEnumerable<ApiResults<PagedResult<User>>>> GetAllUsersAsync(GetAllUserRequest request);
    Task<int> CreateUserAsync(GetAllUserRequest request);
    Task<bool> DeleteUserAsync(string id);
}