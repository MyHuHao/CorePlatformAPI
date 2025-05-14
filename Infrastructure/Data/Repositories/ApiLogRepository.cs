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
                            insert into api_log
                               (ip_address,
                               user_name,
                               path,
                               method,
                               request_body,
                               response_body,
                               status_code,
                               error_message,
                               request_time,
                               duration) 
                            values
                               (@ip_address,
                               @user_name,
                               @path,
                               @method,
                               @request_body,
                               @response_body,
                               @status_code,
                               @error_message,
                               @request_time,
                               @duration);
                           """;
        var rows = await dapper.ExecuteAsync(sql, new
        {
            ip_address = apiLog.IpAddress == "::1" ? "127.0.0.1" : apiLog.IpAddress,
            user_name = apiLog.UserName,
            path = apiLog.Path,
            method = apiLog.Method,
            request_body = apiLog.RequestBody,
            response_body = apiLog.ResponseBody,
            status_code = apiLog.StatusCode,
            error_message = apiLog.ErrorMessage,
            request_time = apiLog.RequestTime,
            duration = apiLog.Duration
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
            conditions.Add("ip_address = @ip_address");
            parameters.Add("ip_address", request.IpAddress);
        }

        if (!string.IsNullOrEmpty(request.UserName))
        {
            conditions.Add("user_name = @user_name");
            parameters.Add("user_name", request.UserName);
        }

        if (!string.IsNullOrEmpty(request.Path))
        {
            conditions.Add("path = @path");
            parameters.Add("path", request.Path);
        }

        if (!string.IsNullOrEmpty(request.Method))
        {
            conditions.Add("method = @method");
            parameters.Add("method", request.Method);
        }

        // 构建动态WHERE子句
        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;

        var sql = $"""
                        SELECT
                         id,
                         ip_address,
                         user_name,
                         path,
                         method,
                         request_body,
                         response_body,
                         status_code,
                         error_message,
                         request_time,
                         duration
                       FROM
                         api_log
                        {whereClause}
                       order by
                         request_time desc
                   """;
        return await dapper.QueryPageAsync(request.Page, request.PageSize, sql, parameters);
    }
}