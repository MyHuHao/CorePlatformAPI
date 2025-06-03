using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories;

public class RoleGroupResRepository(IDapperExtensions<RoleGroupResource> dapper, IUnitOfWork unitOfWork)
    : IRoleGroupResRepository
{
    public async Task<IEnumerable<RoleGroupResource>> GetAllRoleGroupByIdAsync(string companyId, string roleGroupId)
    {
        const string sql =
            "SELECT * FROM RoleGroupResource WHERE CompanyId = @CompanyId AND RoleGroupId = @RoleGroupId";
        return await dapper.QueryAsync(sql,
            new
            {
                CompanyId = companyId,
                RoleGroupId = roleGroupId
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    public async Task<int> AddRoleGroupResourceAsync(RoleGroupAuthorizeRequest request)
    {
        const string sql = """
                            INSERT INTO RoleGroupResource
                               (CompanyId,
                               RoleGroupId,
                               ResId,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime
                               )
                            VALUES
                               (@CompanyId,
                               @RoleGroupId,
                               @ResId,
                               @CreatedBy,
                               @CreatedTime,
                               @ModifiedBy,
                               @ModifiedTime);
                           """;
        var currentTime = DateTime.Now;
        var records = request.ResIds.Select(roleId => new RoleGroupResource
        {
            CompanyId = request.CompanyId,
            RoleGroupId = request.RoleGroupId,
            ResId = roleId,
            CreatedBy = request.StaffId,
            CreatedTime = currentTime,
            ModifiedBy = request.StaffId,
            ModifiedTime = currentTime
        }).ToList();
        return await dapper.ExecuteAsync(sql, records,
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    public async Task<int> DeleteRoleGroupResourceAsync(RoleGroupAuthorizeRequest request)
    {
        const string sql = """
                            DELETE FROM RoleGroupResource
                            WHERE CompanyId = @CompanyId
                            AND RoleGroupId = @RoleGroupId
                             AND ResId  IN @ResIds;
                           """;
        return await dapper.ExecuteAsync(sql, new
            {
                request.CompanyId,
                request.RoleGroupId,
                request.ResIds
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }
}