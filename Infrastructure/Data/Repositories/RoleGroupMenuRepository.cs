using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories;

public class RoleGroupMenuRepository(IDapperExtensions<RoleGroupWebMenu> dapper, IUnitOfWork unitOfWork)
    : IRoleGroupMenuRepository
{
    public async Task<IEnumerable<RoleGroupWebMenu>> GetAllRoleGroupByIdAsync(string companyId, string roleGroupId)
    {
        const string sql =
            "SELECT * FROM RoleGroupWebMenu WHERE CompanyId = @CompanyId AND RoleGroupId = @RoleGroupId";
        return await dapper.QueryAsync(sql,
            new
            {
                CompanyId = companyId,
                RoleGroupId = roleGroupId
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    public async Task<int> AddRoleGroupWebMenuAsync(RoleGroupAuthorizeRequest request)
    {
        const string sql = """
                            INSERT INTO RoleGroupWebMenu
                               (CompanyId,
                               RoleGroupId,
                               WebMenuId,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime)
                            VALUES
                               (@CompanyId,
                               @RoleGroupId,
                               @WebMenuId,
                               @CreatedBy,
                               @CreatedTime,
                               @ModifiedBy,
                               @ModifiedTime);
                           """;
        var currentTime = DateTime.Now;
        var records = request.MenuIds.Select(roleId => new RoleGroupWebMenu
        {
            CompanyId = request.CompanyId,
            RoleGroupId = request.RoleGroupId,
            WebMenuId = roleId,
            CreatedBy = request.StaffId,
            CreatedTime = currentTime,
            ModifiedBy = request.StaffId,
            ModifiedTime = currentTime
        }).ToList();
        return await dapper.ExecuteAsync(sql, records,
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    public async Task<int> DeleteRoleGroupWebMenuAsync(RoleGroupAuthorizeRequest request)
    {
        const string sql = """
                            DELETE FROM RoleGroupWebMenu
                            WHERE CompanyId = @CompanyId
                            AND RoleGroupId = @RoleGroupId
                            AND WebMenuId IN @WebMenuIds
                           """;
        return await dapper.ExecuteAsync(sql, new
            {
                request.CompanyId,
                request.RoleGroupId,
                WebMenuIds = request.MenuIds
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }
}