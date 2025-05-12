namespace Core.Entities;

public class ApiLog
{
    /// <summary>
    ///     日志ID
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    ///     客户端IP
    /// </summary>
    public string IpAddress { get; set; } = "";

    /// <summary>
    ///     账号（可为空）
    /// </summary>
    public string UserName { get; set; } = "";

    /// <summary>
    ///     请求路径
    /// </summary>
    public string Path { get; set; } = "";

    /// <summary>
    ///     HTTP方法
    /// </summary>
    public string Method { get; set; } = "";

    /// <summary>
    ///     请求参数
    /// </summary>
    public string RequestBody { get; set; } = "";

    /// <summary>
    ///     响应结果
    /// </summary>
    public string ResponseBody { get; set; } = "";

    /// <summary>
    ///     响应状态码
    /// </summary>
    public short StatusCode { get; set; }

    /// <summary>
    ///     错误信息
    /// </summary>
    public string ErrorMessage { get; set; } = "";

    /// <summary>
    ///     请求时间
    /// </summary>
    public DateTime RequestTime { get; set; }

    /// <summary>
    ///     处理耗时（毫秒）
    /// </summary>
    public int Duration { get; set; }
}