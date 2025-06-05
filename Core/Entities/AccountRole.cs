namespace Core.Entities;

public class AccountRole
{
    /// <summary>
    /// 公司ID
    /// </summary>
    public string CompanyId { get; set; } = "";

    /// <summary>
    /// 账户ID（用户ID）
    /// </summary>
    public string AccId { get; set; } = "";

    /// <summary>
    /// 角色组ID
    /// </summary>
    public string RoleGroupId { get; set; } = "";

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