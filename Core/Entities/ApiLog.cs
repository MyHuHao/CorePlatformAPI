namespace Core.Entities;

public class ApiLog
{
    /// <summary>
    ///     日志ID
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    ///     客户端IP地址
    /// </summary>
    public string IpAddress { get; set; } = "";

    /// <summary>
    ///     登录账号名称
    /// </summary>
    public string UserName { get; set; } = "";

    /// <summary>
    ///     API请求路径
    /// </summary>
    public string Path { get; set; } = "";

    /// <summary>
    ///     HTTP请求方法（GET/POST等）
    /// </summary>
    public string Method { get; set; } = "";

    /// <summary>
    ///     请求体原始数据（JSON格式）
    /// </summary>
    public string RequestBody { get; set; } = "";

    /// <summary>
    ///     响应体原始数据（JSON格式）
    /// </summary>
    public string ResponseBody { get; set; } = "";

    /// <summary>
    ///     HTTP响应状态码（如200/500等）
    /// </summary>
    public short StatusCode { get; set; }

    /// <summary>
    ///     错误详细信息
    /// </summary>
    public string ErrorMessage { get; set; } = "";

    /// <summary>
    ///     请求发起时间
    /// </summary>
    public DateTime RequestTime { get; set; }

    /// <summary>
    ///     接口处理耗时（单位：毫秒）
    /// </summary>
    public long Duration { get; set; }
}