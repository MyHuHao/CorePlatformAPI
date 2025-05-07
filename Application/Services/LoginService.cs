using Application.Interfaces;
using Core.DTOs;
using Core.DTOs.Base;

namespace Application.Services;

public class LoginService : ILoginService
{
    public async Task<ApiResponse<string>> Login(LoginRequest request)
    {
        // 验证账户，密码是否正确
        // 生成token
        await Task.CompletedTask;
        return new ApiResponse<string>()
        {
            Data = "2323", MsgCode = 0, Msg = "登录成功"
        };
    }
}