using Application.Queries;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Services;

namespace Application.Services;

public class UserService(UserQuery userQuery, IMapper mapper) : IUserService
{
    /// <summary>
    ///     通过用户ID获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApiResult<UserDto>> GetUserByIdAsync(string id)
    {
        var user = await userQuery.GetByIdAsync(id);
        var userDto = mapper.Map<UserDto>(user);
        return new ApiResult<UserDto> { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = userDto };
    }

    public Task<IEnumerable<ApiResult<PagedResult<User>>>> GetAllUsersAsync(GetAllUserRequest request)
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