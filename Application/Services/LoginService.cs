using Application.Interfaces;
using Core.Contracts.Requests;
using Core.Contracts.Results;

namespace Application.Services;

public class LoginService : ILoginService
{
    public async Task<ApiResults<string>> Login(LoginRequest request)
    {
        throw new Exception("232323");
        // 验证账户，密码是否正确

        // 生成token

        // 获取当前登录账户信息

        // 返回数据

        await Task.CompletedTask;
        return new ApiResults<string>
        {
            Data = "2323", MsgCode = 0, Msg = "登录成功"
        };
    }
}