namespace Core.Entities;

public class User
{
    /// <summary>
    /// 主键ID
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; } = "";

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// 头像URL
    /// </summary>
    public string Avatar { get; set; } = "";

    /// <summary>
    /// 性别（0-未知，1-男，2-女）
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime Birthday { get; set; }
    
    /// <summary>
    /// 状态（0-禁用，1-启用）
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string CreateBy { get; set; } = "";

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    public string ModifyBy { get; set; } = "";

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime ModifyTime { get; set; }
}