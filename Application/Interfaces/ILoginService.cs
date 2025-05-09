using Core.Contracts.Requests;
using Core.Contracts.Results;

namespace Application.Interfaces;

public interface ILoginService
{
    /// <summary>
    /// 创建账号
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResult<string>> CreateAccount(CreateAccountRequest request);
    Task<ApiResult<string>> Login(LoginRequest request);
   
}