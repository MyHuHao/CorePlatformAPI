using Application.Commands;
using Application.Queries;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services;

public class LoginService(IUnitOfWork unitOfWork, UserQuery userQuery, LoginQuery loginQuery, LoginCommand loginCommand)
    : ILoginService
{
    public async Task<ApiResult<string>> CreateAccount(CreateAccountRequest request)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync();
            // 检查人员是否存在
            var userResult = await userQuery.GetByIdAsync(request.UserId);
            if (userResult == null) throw new ValidationException(MsgCodeEnum.Warning, "人员不存在，禁止创建");

            // 检查账户是否存在
            var accountResult = await loginQuery.GetByIdAsync(request.Account);
            if (accountResult != null) throw new ValidationException(MsgCodeEnum.Warning, "账户已存在，请重新输入");

            // 创建账户
            await loginCommand.CreateAccount(request);

            // 结束事务，返回结果
            await unitOfWork.CommitAsync();
            return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "创建成功" };
        }
        catch (Exception exception)
        {
            await unitOfWork.RollbackAsync();
            throw new BadRequestException(MsgCodeEnum.Error, exception.Message);
        }
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