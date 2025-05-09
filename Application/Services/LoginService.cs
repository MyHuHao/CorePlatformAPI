using Application.Commands;
using Application.Interfaces;
using Application.Queries;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using MySql.Data.MySqlClient;

namespace Application.Services;

public class LoginService(UserQuery userQuery, LoginCommand loginCommand) : ILoginService
{
    public async Task<ApiResult<string>> CreateAccount(CreateAccountRequest request)
    {
        await using MySqlConnection con = new("");
        await con.OpenAsync();
        await using var transaction = await con.BeginTransactionAsync();
        // 检查账户是否存在
        var userResult = await userQuery.GetByIdAsync(request.UserId);
        if (string.IsNullOrEmpty(userResult.Id))
        {
            throw new ValidationException(MsgCodeEnum.Error, "人员不存在，禁止创建");
        }
        // 创建账户
        await loginCommand.CreateAccount(request);
        await transaction.CommitAsync();
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