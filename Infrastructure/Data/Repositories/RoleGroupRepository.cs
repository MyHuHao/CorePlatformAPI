using Core.Contracts.Requests;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Data.Repositories;

public class RoleGroupRepository(IDapperExtensions<RoleGroup> dapper, IUnitOfWork unitOfWork) : IRoleGroupRepository
{
    /// <summary>
    /// 新增角色组
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
                request.RoleGroupId,
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
    /// 分页查询角色组
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
    /// 删除角色组
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
    /// 通过id查询角色组详细
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
    /// 验证是否重复
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

        conditions.Add("Status = @Status");
        parameters.Add("Status", request.Status);

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
}