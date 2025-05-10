using Application.Commands;
using Application.Queries;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services;

public class LoginService(IUserRepository repository, UserQuery userQuery, LoginCommand loginCommand)
    : ILoginService
{
    public async Task<ApiResult<string>> CreateAccount(CreateAccountRequest request)
    {
        // 检查账户是否存在
        var userResult = await userQuery.GetByIdAsync(request.UserId);
        if (string.IsNullOrEmpty(userResult.Id))
        {
            throw new ValidationException(MsgCodeEnum.Error, "人员不存在，禁止创建");
        }
        // 创建账户
        await loginCommand.CreateAccount(request);
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "创建成功" };
    }

    public async Task<ApiResult<string>> Login(LoginRequest request)
    {
        // 验证账户，密码是否正确

        // 生成token

        // 获取当前登录账户信息

        // 返回数据

        await Task.CompletedTask;
        return new ApiResult<string>
        {
            Data = "2323", MsgCode = 0, Msg = "登录成功"
        };
    }
}