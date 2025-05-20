using Core.Contracts.Requests;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Data.Repositories;

public class LoginTokenRepository(IDapperExtensions<LoginToken> dapper) : ILoginTokenRepository
{
    /// <summary>
    /// 通过登录用户名查询登录用户信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<LoginToken?> GetByLoginTokenAsync(ByLoginTokenRequest request)
    {
        const string sql = """
                           SELECT
                               Id,
                               CompanyId,
                               UserId,
                               Token,
                               RefreshToken,
                               ExpireTime,
                               DeviceId,
                               IsActive,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime
                           FROM
                            LoginToken
                           WHERE
                            CompanyId = @CompanyId
                           AND 
                            UserId = @UserId
                           AND
                            IsActive = @IsActive
                           ORDER BY 
                            CreatedTime desc
                           """;
        return await dapper.QueryFirstOrDefaultAsync(sql,
            new
            {
                request.CompanyId,
                request.UserId,
                request.IsActive
            });
    }

    /// <summary>
    /// 获取登录用户列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<LoginToken> items, int total)> GetByLoginTokenListAsync(
        ByLoginTokenListRequest request)
    {
        var conditions = new List<string>();
        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(request.CompanyId))
        {
            conditions.Add("CompanyId = @CompanyId");
            parameters.Add("CompanyId", request.CompanyId);
        }

        if (!string.IsNullOrEmpty(request.UserId))
        {
            conditions.Add("UserId = @UserId");
            parameters.Add("UserId", request.UserId);
        }

        conditions.Add("IsActive = @IsActive");
        parameters.Add("IsActive", request.IsActive);

        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;

        var sql = $"""
                   SELECT
                       Id,
                       CompanyId,
                       UserId,
                       Token,
                       RefreshToken,
                       ExpireTime,
                       DeviceId,
                       IsActive,
                       CreatedBy,
                       CreatedTime,
                       ModifiedBy,
                       ModifiedTime
                   FROM
                        LoginToken
                    {whereClause}
                    ORDER BY 
                        CreatedTime desc
                   """;
        return await dapper.QueryPageAsync(request.Page, request.PageSize, sql, parameters);
    }

    /// <summary>
    /// 新增登录用户
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<int> AddLoginTokenAsync(AddLoginTokenRequest request)
    {
        const string sql = """
                           INSERT INTO LoginToken
                               (Id,
                               CompanyId,
                               UserId,
                               Token,
                               RefreshToken,
                               ExpireTime,
                               DeviceId,
                               IsActive,
                               CreatedBy,
                               ModifiedBy)
                           VALUES
                               (@Id,
                               @CompanyId,
                               @UserId,
                               @Token,
                               @RefreshToken,
                               @ExpireTime,
                               @DeviceId,
                               @IsActive,
                               @CreatedBy,
                               @ModifiedBy);
                           """;
        return await dapper.ExecuteAsync(sql,
            new
            {
                Id = HashHelper.GetUuid(),
                request.CompanyId,
                request.UserId,
                request.Token,
                request.RefreshToken,
                request.ExpireTime,
                request.DeviceId,
                request.IsActive,
                request.CreatedBy,
                request.ModifiedBy,
            });
    }

    /// <summary>
    /// 验证登录用户
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<bool> VerifyLoginTokenAsync(ByLoginTokenRequest request)
    {
        const string sql = """
                           SELECT 
                            count(*) as total 
                           FROM 
                            LoginToken 
                           WHERE 
                            CompanyId = @CompanyId
                           AND    
                            UserId = @UserId 
                           AND 
                            IsActive = @IsActive 
                           AND
                            Token = @Token;
                           """;
        var result = await dapper.QueryScalarAsync(sql,
            new { request.CompanyId, request.UserId, request.IsActive, request.Token });
        return result > 0;
    }

    /// <summary>
    /// 停用登录用户
    /// </summary>
    /// <param name="id"></param>
    public async Task DisableLoginTokenAsync(string id)
    {
        const string sql = """
                           UPDATE 
                            LoginToken
                           SET
                            IsActive = @IsActive
                           WHERE
                            Id = @Id;
                           """;
        await dapper.ExecuteAsync(sql, new { id });
    }
}