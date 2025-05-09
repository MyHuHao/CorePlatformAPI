namespace Core.Entities;

public class Account
{
    /// <summary>
    ///     主键ID（唯一标识账户）
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    ///     登录用户名（账号凭证）
    /// </summary>
    public string AccountName { get; set; } = "";

    /// <summary>
    ///     关联人员ID（外键关联用户表）
    /// </summary>
    public string UserId { get; set; } = "";

    /// <summary>
    ///     密码哈希值（存储加密后的密码）
    /// </summary>
    public string PasswordHash { get; set; } = "";

    /// <summary>
    ///     密码盐值（用于增强密码安全性）
    /// </summary>
    public string PasswordSalt { get; set; } = "";

    /// <summary>
    ///     关联角色ID（外键关联角色表）
    /// </summary>
    public string RoleId { get; set; } = "";

    /// <summary>
    ///     连续登录失败次数（用于安全限制）
    /// </summary>
    public int LoginAttempts { get; set; }

    /// <summary>
    ///     最近登录时间（记录最后一次登录）
    /// </summary>
    public DateTime LastLoginTime { get; set; }

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