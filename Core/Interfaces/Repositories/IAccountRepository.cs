using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IAccountRepository
{
    /// <summary>
    ///     通过登录用户名查询账号信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Account?> GetByAccountAsync(ByAccountRequest request);

    /// <summary>
    ///     获取账号列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<(IEnumerable<Account> items, int total)> GetByAccountListAsync(ByAccountListRequest request);

    /// <summary>
    ///     新增账号
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<int> AddAccountAsync(AddAccountRequest request);

    /// <summary>
    ///     批量新增账号
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    Task<int> BatchAddAccountAsync(List<AddAccountRequest> list);

    /// <summary>
    ///     删除账号
    /// </summary>
    /// <returns></returns>
    Task<int> DeleteAccountAsync(string id);

    /// <summary>
    ///     验证账号是否存在
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="loginName"></param>
    /// <returns></returns>
    Task<Account?> IsExistAccountAsync(string companyId, string loginName);

    /// <summary>
    ///     通过ID查询账号详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Account?> GetAccountByIdAsync(string id);
}