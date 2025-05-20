namespace Core.Entities;

public class LoginToken
{
    /// <summary>
    /// 唯一标识（支持UUID）
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// 所属公司ID
    /// </summary>
    public string CompanyId { get; set; } = "";

    /// <summary>
    /// 关联用户ID
    /// </summary>
    public string UserId { get; set; } = "";

    /// <summary>
    /// JWT访问令牌
    /// </summary>
    public string Token { get; set; } = "";

    /// <summary>
    /// 刷新用令牌
    /// </summary>
    public string RefreshToken { get; set; } = "";

    /// <summary>
    /// 令牌过期时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// 设备唯一标识
    /// </summary>
    public string DeviceId { get; set; } = "";

    /// <summary>
    /// 令牌状态（1-有效，0-已失效）
    /// </summary>
    public byte IsActive { get; set; } 

    /// <summary>
    /// 创建人AccountId
    /// </summary>
    public string CreatedBy { get; set; } = "";

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// 修改人AccountId
    /// </summary>
    public string ModifiedBy { get; set; } = "";

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime ModifiedTime { get; set; }
}