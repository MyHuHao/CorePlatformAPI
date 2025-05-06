using API.Models;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public class LoginService(IDapperRepository<User> userRepository)
{
    public async Task<ApiResponse<string>> Login(LoginRequest request)
    {
        await Task.CompletedTask;
        return new ApiResponse<string>()
        {
            Data = "2323", MsgCode = 0, Msg = "登录成功"
        };
    }
}