namespace Core.Entities;

public class ApiLog
{
    /// <summary>
    /// 日志ID
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>
    public required string IpAddress { get; set; }

    /// <summary>
    ///  账号（可为空）
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// 请求路径
    /// </summary>
    public required string Path { get; set; }

    /// <summary>
    /// HTTP方法
    /// </summary>
    public required string Method { get; set; }

    /// <summary>
    /// 请求参数
    /// </summary>
    public required string RequestBody { get; set; }

    /// <summary>
    /// 响应结果
    /// </summary>
    public required string ResponseBody { get; set; }

    /// <summary>
    /// 响应状态码
    /// </summary>
    public required short StatusCode { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public required string ErrorMessage { get; set; }

    /// <summary>
    /// 请求时间
    /// </summary>
    public required DateTime RequestTime { get; set; }

    /// <summary>
    /// 处理耗时（毫秒）
    /// </summary>
    public required int  Duration { get; set; }
}