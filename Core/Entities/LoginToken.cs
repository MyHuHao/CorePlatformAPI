namespace Core.Entities;

public class LoginToken
{
    /// <summary>
    /// id主键
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// 用户表ID
    /// </summary>
    public string UserId { get; set; } = "";

    /// <summary>
    /// 登录验证token
    /// </summary>
    public string Token { get; set; } = "";

    /// <summary>
    /// 刷新Token
    /// </summary>
    public string RefreshToken { get; set; } = "";

    /// <summary>
    /// 到期时间
    /// </summary>
    public DateTime ExpireTime { get; set; }

    /// <summary>
    /// 设备唯一标识（用于单点登录）
    /// </summary>
    public string DeviceId { get; set; } = "";

    /// <summary>
    /// 标记是否有效（用于踢人下线）
    /// </summary>
    public bool IsActive { get; set; }

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