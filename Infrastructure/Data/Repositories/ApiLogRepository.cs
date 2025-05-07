using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data.Repositories;

public class ApiLogRepository(IDapperExtensions<ApiLog> dapper) : IApiLogRepository
{
    /// <summary>
    /// 插入数据
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
        // var rows = await dapper.ExecuteAsync(sql, new
        // {
        //     ip_address = apiLog.IpAddress,
        //     user_name = apiLog.UserName,
        //     path = apiLog.Path,
        //     method = apiLog.Method,
        //     request_body = apiLog.RequestBody,
        //     response_body = apiLog.ResponseBody,
        //     status_code = apiLog.StatusCode,
        //     error_message = apiLog.ErrorMessage,
        //     equest_time = apiLog.RequestTime,
        //     duration = apiLog.Duration
        // });
        var rows = await dapper.ExecuteAsync(sql, new
        {
            ip_address = "localhost",
            user_name = "测试",
            path = "123456",
            method = "GET",
            request_body = "",
            response_body = "测试",
            status_code = 200,
            error_message = "",
            equest_time = "2025-05-07 09:26:59",
            duration = 100
        });
        return rows > 0;
    }
}