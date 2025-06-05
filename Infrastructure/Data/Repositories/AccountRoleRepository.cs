using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories;

public class AccountRoleRepository(IDapperExtensions<AccountRole> dapper, IUnitOfWork unitOfWork)
    : IAccountRoleRepository
{
    public async Task<IEnumerable<AccountRole>> GetAccountRoleAsync(string companyId, string accId)
    {
        const string sql = """
                               SELECT
                               CompanyId,
                               AccId,
                               RoleGroupId,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime
                               FROM
                               AccountRole
                               WHERE
                               CompanyId = @CompanyId
                               AND
                               AccId = @AccId
                           """;
        return await dapper.QueryAsync(sql,
            new { CompanyId = companyId, AccId = accId },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }
}