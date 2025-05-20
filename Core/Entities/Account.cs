namespace Core.Entities;

public class Account
{
    /// <summary>
    /// 主键ID（唯一标识，支持UUID）
    /// </summary>
    public string Id { get; set; } = ""; 

    /// <summary>
    /// 所属公司ID
    /// </summary>
    public string CompanyId { get; set; } = "";

    /// <summary>
    /// 登录用户名（唯一，支持邮箱/手机号）
    /// </summary>
    public string Username { get; set; } = "";

    /// <summary>
    /// 显示名称（昵称）
    /// </summary>
    public string DisplayName { get; set; } = "";

    /// <summary>
    /// 密码哈希（建议使用bcrypt/Argon2）
    /// </summary>
    public string PasswordHash { get; set; } = "";

    /// <summary>
    /// 密码盐值（如使用PBKDF2等算法）
    /// </summary>
    public string? PasswordSalt { get; set; } // 可空字段

    /// <summary>
    /// 账户类型（1-超级管理员，2-公司管理员，3-普通用户）
    /// </summary>
    public byte AccountType { get; set; } = 0; // 对应tinyint [[8]]

    /// <summary>
    /// 是否激活（1-是，0-否）
    /// </summary>
    public byte IsActive { get; set; } = 0; // 对应tinyint

    /// <summary>
    /// 部门ID
    /// </summary>
    public string DeptId { get; set; } = "";

    /// <summary>
    /// 绑定邮箱（唯一）
    /// </summary>
    public string Email { get; set; } = ""; // [[1]]

    /// <summary>
    /// 绑定手机号（唯一）
    /// </summary>
    public string Phone { get; set; } = ""; // [[1]]

    /// <summary>
    /// 语言偏好（如zh-CN/en-US）
    /// </summary>
    public string Language { get; set; } = "";

    /// <summary>
    /// 最后登录时间
    /// </summary>
    public DateTime? LastLoginTime { get; set; } // 可空时间戳

    /// <summary>
    /// 最后登录IP
    /// </summary>
    public string LastLoginIp { get; set; } = "";

    /// <summary>
    /// 连续登录失败次数
    /// </summary>
    public byte FailedLoginAttempts { get; set; } = 0; 

    /// <summary>
    /// 是否锁定（1-是，0-否）
    /// </summary>
    public byte IsLocked { get; set; } = 0; 

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