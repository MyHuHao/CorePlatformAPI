namespace Core.Entities;

public class LoginLog
{
    /// <summary>
    /// ID主键
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// 用户ID
    /// </summary>
    public string UserId { get; set; } = "";

    /// <summary>
    /// 登录时间
    /// </summary>
    public string LoginTime { get; set; } = "";

    /// <summary>
    /// ip地址
    /// </summary>
    public string IpAddress { get; set; } = "";

    /// <summary>
    /// 设备信息
    /// </summary>
    public string DeviceInfo { get; set; } = "";
    
    /// <summary>
    ///     创建人ID（记录操作者身份）
    /// </summary>
    public string CreatedBy { get; set; } = "";

    /// <summary>
    ///     创建日期（记录账户创建时间）
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    ///     修改人ID（记录最近修改者身份）
    /// </summary>
    public string ModifyBy { get; set; } = "";

    /// <summary>
    ///     修改日期（记录最近修改时间）
    /// </summary>
    public DateTime ModifyTime { get; set; }
}