
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IAccountRoleRepository
{
    // 通过账号获取角色组账号关联数据
    Task<IEnumerable<AccountRole>> GetAccountRoleAsync(string companyId, string accId);
}