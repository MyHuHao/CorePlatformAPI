
using Application.Interfaces;
using Application.Queries;
using AutoMapper;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;

namespace Application.Services;

public class UserService(UserQuery userQuery, IMapper mapper) : IUserService
{
    /// <summary>
    /// 通过用户ID获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ApiResults<UserDto>> GetUserByIdAsync(string id)
    {
        throw new ValidationException(MsgCodeEnum.Success, "232323");
        var user = await userQuery.GetByIdAsync(id);
        var userDto = mapper.Map<UserDto>(user);
        return new ApiResults<UserDto> { MsgCode = 0, Msg = "查询成功", Data = userDto };
    }

    public Task<IEnumerable<ApiResults<PagedResult<User>>>> GetAllUsersAsync(GetAllUserRequest request)
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