using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Commands;
using Application.Queries;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class LoginService(
    IConfiguration configuration,
    IUnitOfWork unitOfWork,
    UserQuery userQuery,
    LoginQuery loginQuery,
    LoginCommand loginCommand)
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
        var isValid = await loginQuery.IsValidAccountPassWord(request);
        if (isValid == false)
        {
            throw new ValidationException(MsgCodeEnum.Warning, "账号密码错误，请重新输入");
        }

        // 生成token
        var token = CreateToken(request);

        // 获取当前登录账户信息

        // 返回数据
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "登录成功", Data = token };
    }

    private string CreateToken(LoginRequest request)
    {
        var key = Encoding.UTF8.GetBytes(configuration["JWT:IssuerSigningKey"] ?? "");
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim("Account", request.Account),
                new Claim("Password", request.PassWord),
                new Claim("Regions", request.LoginType.ToRegion()),
                new Claim("DataBase", request.LoginType.ToDataBase()),
                new Claim("Language", request.Language)
            ]),
            Expires = DateTime.Now.AddHours(8), // 令牌过期时间
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}