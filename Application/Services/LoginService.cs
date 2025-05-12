using Application.Commands;
using Application.Queries;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services;

public class LoginService(IUnitOfWork unitOfWork, UserQuery userQuery, LoginQuery loginQuery, LoginCommand loginCommand) : ILoginService
{
    /// <summary>
    /// 创建账号
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    /// <exception cref="BadRequestException"></exception>
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

    /// <summary>
    /// 登录接口
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<ApiResult<string>> Login(LoginRequest request)
    {
        // 验证账号，获取账号信息
        var accountResult = await loginQuery.GetByIdAsync(request.Account);
        if (accountResult == null)
        {
            throw new ValidationException(MsgCodeEnum.Warning, "用户不存在，禁止创建");
        }

        // 验证账户，密码是否正确
        var isValid = HashHelper.VerifyPassword(
            request.PassWord,
            accountResult.PasswordHash,
            accountResult.PasswordSalt);
        if (isValid == false)
        {
            throw new ValidationException(MsgCodeEnum.Warning, "账号密码错误，请重新输入");
        }

        // 生成token 和  // 记录登录日志
        var token = await CreateToken(accountResult, request);

        // 返回数据
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "登录成功", Data = token };
    }

    // 验证token是否合格
    public async Task<bool> VerifyToken(string token)
    {
        var result = await loginQuery.GetLoginTokeById(token);
        return result != null;
    }

    private async Task<string> CreateToken(Account accountResult, LoginRequest request)
    {
        // 生成Token和RefreshToken
        var token = HashHelper.GetUuid();
        var refreshToken = HashHelper.GetUuid();
        var device = HashHelper.GetUuid();

        // 记录Token到数据库
        var loginToken = new InsertLoginToken
        {
            UserId = accountResult.UserId,
            Token = token,
            RefreshToken = refreshToken,
            ExpireTime = DateTime.Now.AddHours(8),
            DeviceId = device
        };
        await loginCommand.InsertLoginToken(loginToken);
        await loginCommand.InsertLogLog(loginToken);
        return token;
    }
}