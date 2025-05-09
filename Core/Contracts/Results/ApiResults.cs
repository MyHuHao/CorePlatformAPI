namespace Core.Contracts.Results;

public class ApiResults<T>
{
    /// <summary>
    /// 返回数据可以为空
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// 返回信息提示
    /// </summary>
    public string Msg { get; set; } = "";

    /// <summary>
    /// 信息提示的类型
    /// 0：成功提示，1：警告提示，2：错误提示，3：信息提示
    /// </summary>
    public int MsgCode { get; set; }

    /// <summary>
    /// 接口请求时间
    /// </summary>
    public string Time { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
}