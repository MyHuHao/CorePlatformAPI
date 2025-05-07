using Application.Interfaces;
using AutoMapper;
using Core.DTOs;
using Core.DTOs.Base;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public class UserService(IUserRepository repository, IMapper mapper) : IUserService
{
    public async Task<ApiResponse<UserDto>> GetUserByIdAsync(string id)
    {
        var user = await repository.GetByIdAsync(id);
        var userDto = mapper.Map<UserDto>(user);
        return new ApiResponse<UserDto> { MsgCode = 0, Msg = "查询成功", Data = userDto };
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