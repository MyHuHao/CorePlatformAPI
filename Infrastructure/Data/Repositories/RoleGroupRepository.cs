using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Data.Repositories;

public class RoleGroupRepository(
    IDapperExtensions<RoleGroup> dapper,
    IDapperExtensions<RoleGroupResResult> dapperRoleGroupRes,
    IDapperExtensions<RoleGroupMenuResult> dapperRoleGroupMenu,
    IUnitOfWork unitOfWork) : IRoleGroupRepository
{
    /// <summary>
    ///     新增角色组
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<int> AddRoleGroupAsync(AddRoleGroupRequest request)
    {
        const string sql = """
                           INSERT INTO RoleGroup
                               (Id,
                               CompanyId,
                               RoleGroupId,
                               RoleGroupName,
                               RoleGroupDesc,
                               Status,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime)
                           VALUES
                               (@Id,
                               @CompanyId,
                               @RoleGroupId,
                               @RoleGroupName,
                               @RoleGroupDesc,
                               @Status,
                               @CreatedBy,
                               @CreatedTime,
                               @ModifiedBy,
                               @ModifiedTime);
                           """;
        var currentTime = DateTime.Now;
        return await dapper.ExecuteAsync(sql,
            new
            {
                Id = HashHelper.GetUuid(),
                request.CompanyId,
                RoleGroupId = HashHelper.GetUuid(),
                request.RoleGroupName,
                request.RoleGroupDesc,
                request.Status,
                CreatedBy = request.StaffId,
                CreatedTime = currentTime,
                ModifiedBy = request.StaffId,
                ModifiedTime = currentTime
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    ///     分页查询角色组
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<RoleGroup> items, int total)> GetRoleGroupPageAsync(ByRoleGroupListRequest request)
    {
        var conditions = new List<string>();
        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(request.CompanyId))
        {
            conditions.Add("CompanyId = @CompanyId");
            parameters.Add("CompanyId", request.CompanyId);
        }

        if (!string.IsNullOrEmpty(request.RoleGroupName))
        {
            conditions.Add("RoleGroupName = @RoleGroupName");
            parameters.Add("RoleGroupName", request.RoleGroupName);
        }

        if (!string.IsNullOrEmpty(request.Status))
        {
            conditions.Add("Status = @Status");
            parameters.Add("Status", request.Status);
        }

        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;

        var sql = $"""
                       SELECT
                           Id,
                           CompanyId,
                           RoleGroupId,
                           RoleGroupName,
                           RoleGroupDesc,
                           Status,
                           CreatedBy,
                           CreatedTime,
                           ModifiedBy,
                           ModifiedTime
                       FROM
                        RoleGroup
                        {whereClause}
                       ORDER BY 
                        CreatedTime DESC
                   """;
        return await dapper.QueryPageAsync(
            request.Page,
            request.PageSize,
            sql,
            parameters,
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction
        );
    }

    /// <summary>
    ///     删除角色组
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> DeleteRoleGroupAsync(string id)
    {
        const string sql = "DELETE FROM RoleGroup WHERE Id = @Id;";
        return await dapper.ExecuteAsync(sql,
            new { id },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction
        );
    }

    /// <summary>
    ///     通过id查询角色组详细
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<RoleGroup?> GetRoleGroupByIdAsync(string id)
    {
        const string sql = """
                           SELECT
                               Id,
                               CompanyId,
                               RoleGroupId,
                               RoleGroupName,
                               RoleGroupDesc,
                               Status,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime
                           FROM
                                RoleGroup
                           WHERE 
                                Id = @Id;
                           """;
        return await dapper.QueryFirstOrDefaultAsync(sql,
            new { id },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction
        );
    }

    /// <summary>
    ///     验证是否重复
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<RoleGroup?> ValidRoleGroup(ValidRoleGroupRequest request)
    {
        var conditions = new List<string>();
        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(request.CompanyId))
        {
            conditions.Add("CompanyId = @CompanyId");
            parameters.Add("CompanyId", request.CompanyId);
        }

        if (!string.IsNullOrEmpty(request.RoleGroupId))
        {
            conditions.Add("RoleGroupId = @RoleGroupId");
            parameters.Add("RoleGroupId", request.RoleGroupId);
        }

        if (!string.IsNullOrEmpty(request.RoleGroupName))
        {
            conditions.Add("RoleGroupName = @RoleGroupName");
            parameters.Add("RoleGroupName", request.RoleGroupName);
        }


        if (!string.IsNullOrEmpty(request.Status))
        {
            conditions.Add("Status = @Status");
            parameters.Add("Status", request.Status);
        }


        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;

        var sql = $"""
                   SELECT
                       Id,
                       CompanyId,
                       RoleGroupId,
                       RoleGroupName,
                       RoleGroupDesc,
                       Status,
                       CreatedBy,
                       CreatedTime,
                       ModifiedBy,
                       ModifiedTime
                   FROM
                    RoleGroup
                    {whereClause}
                   """;
        return await dapper.QuerySingleOrDefaultAsync(sql,
            parameters,
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction
        );
    }

    // 修改角色组
    public async Task<int> UpdateRoleGroupAsync(UpdateRoleGroupRequest request)
    {
        const string sql = """
                           UPDATE RoleGroup
                           SET
                               RoleGroupName = @RoleGroupName,
                               RoleGroupDesc = @RoleGroupDesc,
                               Status = @Status,
                               ModifiedBy = @ModifiedBy,
                               ModifiedTime = @ModifiedTime
                           WHERE
                               RoleGroupId = @RoleGroupId
                           AND 
                               CompanyId = @CompanyId;
                           """;
        return await dapper.ExecuteAsync(sql,
            new
            {
                request.RoleGroupName,
                request.RoleGroupDesc,
                request.Status,
                request.RoleGroupId,
                request.CompanyId,
                ModifiedBy = request.StaffId,
                ModifiedTime = DateTime.Now
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction
        );
    }

    public async Task<IEnumerable<RoleGroupResResult>> GetRoleGroupResByIdAsync(string companyId, string roleGroupId)
    {
        const string sql = """
                           select a.CompanyId,a.RoleGroupId,a.ResId,b.ResName,b.WebMenuId from RoleGroupResource as a
                           left join Resource as b on a.CompanyId = b.CompanyId and a.ResId = b.Id
                           where a.CompanyId = @CompanyId and a.RoleGroupId = @RoleGroupId
                           """;
        var result = await dapperRoleGroupRes.QueryAsync(sql,
            new
            {
                CompanyId = companyId,
                RoleGroupId = roleGroupId
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
        return result;
    }

    public async Task<IEnumerable<RoleGroupMenuResult>> GetRoleGroupMenuByIdAsync(string companyId, string roleGroupId)
    {
        const string sql = """
                           select a.CompanyId,a.RoleGroupId,a.WebMenuId,b.Title from RoleGroupWebMenu as a
                           left join WebMenu as b on a.WebMenuId = b.WebMenuId
                           where a.CompanyId = @CompanyId and a.RoleGroupId = @RoleGroupId
                           """;
        var result = await dapperRoleGroupMenu.QueryAsync(sql,
            new
            {
                CompanyId = companyId,
                RoleGroupId = roleGroupId
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
        return result;
    }
}