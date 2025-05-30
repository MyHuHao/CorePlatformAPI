using Core.Contracts.Requests;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Data.Repositories;

public class ResourceRepository(IDapperExtensions<Resource> dapper, IUnitOfWork unitOfWork) : IResourceRepository
{
    /// <summary>
    /// 查询资源列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<Resource> items, int total)> GetResourceByPageAsync(ByResourceListRequest request)
    {
        var conditions = new List<string>();
        var parameters = new DynamicParameters();

        conditions.Add("CompanyId = @CompanyId");
        parameters.Add("CompanyId", request.CompanyId);

        conditions.Add("WebMenuId = @WebMenuId");
        parameters.Add("WebMenuId", request.WebMenuId);

        if (!string.IsNullOrEmpty(request.ResName))
        {
            conditions.Add("ResName = @ResName");
            parameters.Add("ResName", request.ResName);
        }

        if (!string.IsNullOrEmpty(request.ResType))
        {
            conditions.Add("ResType = @ResType");
            parameters.Add("ResType", request.ResType);
        }

        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;

        var sql = $"""
                       SELECT
                           Id,
                           CompanyId,
                           ResCode,
                           ResName,
                           ResDesc,
                           ResType,
                           ResSequence,
                           ResPath,
                           WebMenuId,
                           CreatedBy,
                           CreatedTime,
                           ModifiedBy,
                           ModifiedTime
                       FROM
                           Resource
                       {whereClause}
                       ORDER BY
                           ResSequence
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
    /// 新增资源
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<int> AddResourceAsync(AddResourceRequest request)
    {
        const string sql = """
                            INSERT INTO Resource
                               (Id,
                               CompanyId,
                               ResCode,
                               ResName,
                               ResDesc,
                               ResType,
                               ResSequence,
                               ResPath,
                               WebMenuId,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime)
                            VALUES
                               (@Id,
                               @CompanyId,
                               @ResCode,
                               @ResName,
                               @ResDesc,
                               @ResType,
                               @ResSequence,
                               @ResPath,
                               @WebMenuId,
                               @CreatedBy,
                               @CreatedTime,
                               @ModifiedBy,
                               @ModifiedTime)
                           """;
        var currentTime = DateTime.Now;
        return await dapper.ExecuteAsync(sql,
            new
            {
                Id = HashHelper.GetUuid(),
                request.CompanyId,
                request.ResCode,
                request.ResName,
                request.ResDesc,
                request.ResType,
                request.ResSequence,
                request.ResPath,
                request.WebMenuId,
                CreatedBy = request.StaffId,
                CreatedTime = currentTime,
                ModifiedBy = request.StaffId,
                ModifiedTime = currentTime
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    /// 通过ID修改资源
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<int> UpdateResourceAsync(UpdateResourceRequest request)
    {
        const string sql = """
                            UPDATE Resource
                            SET
                               ResCode = @ResCode,
                               ResName = @ResName,
                               ResDesc = @ResDesc,
                               ResType = @ResType,
                               ResSequence = @ResSequence,
                               ResPath = @ResPath,
                               ModifiedBy = @ModifiedBy,
                               ModifiedTime = @ModifiedTime
                            WHERE
                               Id = @Id
                           """;
        return await dapper.ExecuteAsync(sql,
            new
            {
                request.Id,
                request.ResCode,
                request.ResName,
                request.ResDesc,
                request.ResType,
                request.ResSequence,
                request.ResPath,
                ModifiedBy = request.StaffId,
                ModifiedTime = DateTime.Now
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    /// 通过ID查询资源
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Resource?> GetResourceByIdAsync(string id)
    {
        const string sql = """
                            SELECT
                               Id,
                               CompanyId,
                               ResCode,
                               ResName,
                               ResDesc,
                               ResType,
                               ResSequence,
                               ResPath,
                               WebMenuId,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime
                            FROM
                               Resource
                            WHERE
                               Id = @Id
                           """;
        return await dapper.QuerySingleOrDefaultAsync(sql,
            new { Id = id },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    /// 通过ID删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> DeleteResourceByIdAsync(string id)
    {
        const string sql = "DELETE FROM Resource WHERE Id = @Id";
        return await dapper.ExecuteAsync(sql,
            new { Id = id },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    /// 验证资源Code是否重复
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Resource?> ValidResourceAsync(ValidResourceCodeRequest request)
    {
        var conditions = new List<string>();
        var parameters = new DynamicParameters();

        conditions.Add("CompanyId = @CompanyId");
        parameters.Add("CompanyId", request.CompanyId);

        conditions.Add("WebMenuId = @WebMenuId");
        parameters.Add("WebMenuId", request.WebMenuId);
        
        if (!string.IsNullOrEmpty(request.ResourceId))
        {
            conditions.Add("Id != @ResourceId");
            parameters.Add("ResourceId", request.ResourceId);
        }

        if (!string.IsNullOrEmpty(request.ResCode))
        {
            conditions.Add("ResCode = @ResCode");
            parameters.Add("ResCode", request.ResCode);
        }
        
        if (request.ResSequence != null)
        {
            conditions.Add("ResSequence = @ResSequence");
            parameters.Add("ResSequence", request.ResSequence);
        }

        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;

        var sql = $"""
                    SELECT
                       *
                    FROM
                       Resource
                    {whereClause}
                   """;
        return await dapper.QueryFirstOrDefaultAsync(sql, parameters,
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }
}