using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class AccountQuery(IAccountRepository repository)
{
    // 分页查询
    public async Task<(IEnumerable<Account> items, int total)> GetAccountPageAsync(ByAccountListRequest request)
    {
        return await repository.GetByAccountListAsync(request);
    }

    // 验证账号是否存在
    public async Task<bool> IsExistAccountAsync(string companyId, string loginName)
    {
        return await repository.IsExistAccountAsync(companyId, loginName) != null;
    }

    // 根据账号ID查询账号
    public async Task<AccountResult?> GetAccountByIdAsync(string id)
    {
        return await repository.GetAccountByIdAsync(id);
    }
}