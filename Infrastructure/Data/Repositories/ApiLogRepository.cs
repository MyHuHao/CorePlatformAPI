using Core.Entities;
using Core.Interfaces;

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
}