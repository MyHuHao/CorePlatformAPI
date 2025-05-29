using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ILoginTokenRepository
{
    /// <summary>
    ///     通过登录用户名查询登录用户信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<LoginToken?> GetByLoginTokenAsync(ByLoginTokenRequest request);


    /// <summary>
    ///     获取登录用户列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<(IEnumerable<LoginToken> items, int total)> GetByLoginTokenListAsync(ByLoginTokenListRequest request);

    /// <summary>
    ///     新增登录用户
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<int> AddLoginTokenAsync(AddLoginTokenRequest request);


    /// <summary>
    ///     验证登录用户
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> VerifyLoginTokenAsync(ByLoginTokenRequest request);

    /// <summary>
    ///     停用登录用户
    /// </summary>
    /// <returns></returns>
    Task DisableLoginTokenAsync(string id);
}