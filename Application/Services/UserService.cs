using Application.Interfaces;
using Core.DTOs;
using Core.DTOs.Base;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task<ApiResponse<User>> GetUserByIdAsync(string id)
    {
        var user = await repository.GetByIdAsync(id);
        return new ApiResponse<User> { MsgCode = 0, Msg = "查询成功", Data = user };
    }

    public Task<IEnumerable<ApiResponse<PagedResult<User>>>> GetAllUsersAsync(GetAllUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateUserAsync(GetAllUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(string id)
    {
        throw new NotImplementedException();
    }
}