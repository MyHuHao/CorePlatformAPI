using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Data.Repositories;

public class ApiLogRepository(IDapperExtensions<ApiLog> dapper) : IApiLogRepository
{
    /// <summary>
    ///     插入数据
    /// </summary>
    /// <param name="apiLog"></param>
    /// <returns></returns>
    public async Task<bool> AddAsync(ApiLog apiLog)
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
        var rows = await dapper.ExecuteAsync(sql, new
        {
            IpAddress = apiLog.IpAddress == "::1" ? "127.0.0.1" : apiLog.IpAddress,
            apiLog.UserName,
            apiLog.Path,
            apiLog.Method,
            apiLog.RequestBody,
            apiLog.ResponseBody,
            apiLog.StatusCode,
            apiLog.ErrorMessage,
            apiLog.RequestTime,
            apiLog.Duration
        });
        return rows > 0;
    }

    public async Task<(IEnumerable<ApiLog> items, int total)> GetApiLogByPage(ApiLogRequest request)
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

        // 构建动态WHERE子句
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
}