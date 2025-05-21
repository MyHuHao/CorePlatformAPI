using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Commands;
using Application.Queries;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class LoginService(
    IConfiguration configuration,
    IUnitOfWork unitOfWork,
    IEmployeeService employeeService,
    LoginQuery loginQuery,
    LoginCommand loginCommand) : ILoginService
{
    /// <summary>
    /// 创建账号
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    /// <exception cref="BadRequestException"></exception>
    public async Task<ApiResult<string>> CreateAccount(AddAccountRequest request)
    {
        try
        {
            // 开始事务
            await unitOfWork.BeginTransactionAsync();

            // 检查人员是否存在
            var userResult = await employeeService.VerifyEmployeeAsync(new ByEmployeeRequest
            {
                CompanyId = request.CompanyId,
                EmpId = request.EmpId
            });
            if (!userResult) throw new ValidationException(MsgCodeEnum.Warning, "人员不存在，禁止创建");

            // 检查账户是否重复
            var accountResult = await VerifyAccountAsync(new ByAccountRequest
            {
                CompanyId = request.CompanyId,
                LoginName = request.LoginName
            });
            if (accountResult) throw new ValidationException(MsgCodeEnum.Warning, "账户已存在，请重新输入");

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
    /// 验证当前账号是否存在
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private async Task<bool> VerifyAccountAsync(ByAccountRequest request)
    {
        return await loginQuery.GetAccountById(request) != null;
    }

    /// <summary>
    /// 获取登录类型
    /// </summary>
    /// <returns></returns>
    public ApiResult<List<LoginTypeResult>> GetLoginOptions()
    {
        List<LoginTypeResult> list =
        [
            new() { Label = "深圳-测试数据库", Value = 0 },
            new() { Label = "深圳-正式数据库", Value = 1 },
            new() { Label = "上海-测试数据库", Value = 2 },
            new() { Label = "上海-正式数据库", Value = 3 },
        ];
        return new ApiResult<List<LoginTypeResult>> { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = list };
    }


    /// <summary>
    /// 登录接口
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<ApiResult<LoginResult>> Login(LoginRequest request)
    {
        // 验证账号，获取账号信息
        var accountResult = await loginQuery.GetAccountById(new ByAccountRequest
        {
            CompanyId = request.LoginType.ToRegion(),
            LoginName = request.Account
        });
        if (accountResult == null) throw new ValidationException(MsgCodeEnum.Warning, "账户已存在，请重新输入");

        // 验证账户，密码是否正确
        var isValid = HashHelper.VerifyPassword(
            request.PassWord,
            accountResult.PasswordHash,
            accountResult.PasswordSalt);
        if (isValid == false)
        {
            throw new ValidationException(MsgCodeEnum.Warning, "账号密码错误，请重新输入");
        }

        var jti = HashHelper.GetUuid();
        var token = CreateToken(request, jti, accountResult.EmpId);
        await AddLoginToken(request, accountResult, jti);


        var userResult = await employeeService.GetEmployeeById(new ByEmployeeRequest
        {
            CompanyId = request.LoginType.ToRegion(),
            EmpId = accountResult.EmpId
        });

        var result = new LoginResult
        {
            Token = token,
            Employee = userResult.Data ?? new EmployeeDto()
        };
        return new ApiResult<LoginResult> { MsgCode = MsgCodeEnum.Success, Msg = "登录成功", Data = result };
    }

    /// <summary>
    /// 验证token是否合格
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<bool> VerifyLoginTokenAsync(ByLoginTokenRequest request)
    {
        return await loginQuery.VerifyLoginTokenAsync(request);
    }

    /// <summary>
    /// 生成token方法
    /// </summary>
    /// <param name="request"></param>
    /// <param name="jti"></param>
    /// <param name="empId"></param>
    /// <returns></returns>
    private string CreateToken(LoginRequest request, string jti, string empId)
    {
        var key = Encoding.UTF8.GetBytes(configuration["JWT:IssuerSigningKey"] ?? "");
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim("Account", request.Account),
                new Claim("UserId", empId),
                new Claim("CompanyId", request.LoginType.ToRegion()),
                new Claim("DataBase", request.LoginType.ToDataBase()),
                new Claim("Language", request.Language)
            ]),
            Expires = DateTime.Now.AddHours(8), // 令牌过期时间
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }


    /// <summary>
    ///  把令牌信息写入数据库表中
    /// </summary>
    /// <param name="request"></param>
    /// <param name="accountResult"></param>
    /// <param name="jti"></param>
    private async Task AddLoginToken(LoginRequest request, Account accountResult, string jti)
    {
        // 生成device和RefreshToken
        var refreshToken = HashHelper.GetUuid();
        var device = HashHelper.GetUuid();
        var currentTime = DateTime.Now;
        await loginCommand.AddLoginTokenAsync(new AddLoginTokenRequest
        {
            CompanyId = request.LoginType.ToRegion(),
            UserId = accountResult.EmpId,
            Token = jti,
            RefreshToken = refreshToken,
            ExpireTime = currentTime.AddHours(8),
            DeviceId = device,
            IsActive = 1,
            StaffId = accountResult.EmpId
        });
        await loginCommand.AddLoginLog(new AddLoginLogRequest
        {
            CompanyId = request.LoginType.ToRegion(),
            UserId = accountResult.EmpId,
            LoginTime = currentTime,
            IpAddress = "172.0.0.0",
            DeviceInfo = "PC",
            StaffId = accountResult.EmpId
        });
    }
}