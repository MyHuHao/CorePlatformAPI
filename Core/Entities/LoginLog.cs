namespace Core.Entities;

public class LoginLog
{
    /// <summary>
    ///     唯一标识（支持UUID）
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    ///     所属公司ID
    /// </summary>
    public string CompanyId { get; set; } = "";

    /// <summary>
    ///     关联用户ID
    /// </summary>
    public string UserId { get; set; } = "";

    /// <summary>
    ///     登录操作时间
    /// </summary>
    public DateTime LoginTime { get; set; }

    /// <summary>
    ///     登录IP地址
    /// </summary>
    public string IpAddress { get; set; } = "";

    /// <summary>
    ///     设备信息（如浏览器/操作系统）
    /// </summary>
    public string DeviceInfo { get; set; } = "";

    /// <summary>
    ///     创建人AccountId
    /// </summary>
    public string CreatedBy { get; set; } = "";

    /// <summary>
    ///     创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    ///     修改人AccountId
    /// </summary>
    public string ModifiedBy { get; set; } = "";

    /// <summary>
    ///     修改时间
    /// </summary>
    public DateTime ModifiedTime { get; set; }
}