using Core.Contracts.Requests;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Data.Repositories;

public class ApiLogRepository(IDapperExtensions<ApiLog> dapper) : IApiLogRepository
{
    /// <summary>
    /// 通过Id查询api接口日志
    /// </summary>
    /// <returns></returns>
    public async Task<ApiLog?> GetByApiLogAsync(string id)
    {
        const string sql = """
                           SELECT
                               Id,
                               IpAddress,
                               UserName,
                               Path,
                               Method,
                               RequestBody,
                               ResponseBody,
                               StatusCode,
                               ErrorMessage,
                               RequestTime,
                               Duration
                           FROM
                            ApiLog
                           WHERE
                            Id = @Id
                           """;
        return await dapper.QueryFirstOrDefaultAsync(sql, new { id });
    }

    /// <summary>
    /// 查询api接口日志列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<ApiLog> items, int total)> GetByApiLogListAsync(ByApiLogListRequest request)
    {
        var conditions = new List<string>();
        var parameters = new DynamicParameters();

        // 根据参数有效性构建条件列表
        if (!string.IsNullOrEmpty(request.IpAddress))
        {
            conditions.Add("IpAddress = @IpAddress");
            parameters.Add("IpAddress", request.IpAddress);
        }

        if (!string.IsNullOrEmpty(request.UserName))
        {
            conditions.Add("UserName = @UserName");
            parameters.Add("UserName", request.UserName);
        }

        if (!string.IsNullOrEmpty(request.Path))
        {
            conditions.Add("Path = @Path");
            parameters.Add("Path", request.Path);
        }

        if (!string.IsNullOrEmpty(request.Method))
        {
            conditions.Add("Method = @Method");
            parameters.Add("Method", request.Method);
        }

        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;

        var sql = $"""
                    SELECT
                        Id,
                        IpAddress,
                        UserName,
                        Path,
                        Method,
                        RequestBody,
                        ResponseBody,
                        StatusCode,
                        ErrorMessage,
                        RequestTime,
                        Duration
                    FROM
                        ApiLog
                    {whereClause}
                    order by
                        RequestTime desc
                   """;

        return await dapper.QueryPageAsync(request.Page, request.PageSize, sql, parameters);
    }

    /// <summary>
    /// 新增api接口日志
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<int> AddApiLogAsync(AddApiLogRequest request)
    {
        const string sql = """
                            insert into ApiLog
                               (Id,
                               IpAddress,
                               UserName,
                               Path,
                               Method,
                               RequestBody,
                               ResponseBody,
                               StatusCode,
                               ErrorMessage,
                               RequestTime,
                               Duration) 
                            values
                               (@Id,
                               @IpAddress,
                               @UserName,
                               @Path,
                               @Method,
                               @RequestBody,
                               @ResponseBody,
                               @StatusCode,
                               @ErrorMessage,
                               @RequestTime,
                               @Duration);
                           """;
        return await dapper.ExecuteAsync(sql, new
        {
            Id = HashHelper.GetUuid(),
            IpAddress = request.IpAddress == "::1" ? "127.0.0.1" : request.IpAddress,
            request.UserName,
            request.Path,
            request.Method,
            request.RequestBody,
            request.ResponseBody,
            request.StatusCode,
            request.ErrorMessage,
            request.RequestTime,
            request.Duration
        });
    }
}