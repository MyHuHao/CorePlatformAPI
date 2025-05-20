using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;

namespace Core.Interfaces.Services;

public interface ILoginService
{
    /// <summary>
    ///     创建账号
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResult<string>> CreateAccount(AddAccountRequest request);

    /// <summary>
    /// 登录验证,获取Token
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResult<string>> Login(LoginRequest request);
    
    /// <summary>
    /// 获取登录类型
    /// </summary>
    /// <returns></returns>
    ApiResult<List<LoginTypeResult>> GetLoginOptions();

    /// <summary>
    /// 验证token是否合格
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> VerifyLoginTokenAsync(ByLoginTokenRequest request);
}