namespace Core.Entities;

public class Department
{
    /// <summary>
    /// 账户ID（唯一标识，支持UUID）
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// 公司ID
    /// </summary>
    public string CompanyId { get; set; } = "";

    /// <summary>
    /// 部门ID
    /// </summary>
    public string DeptId { get; set; } = "";

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; } = "";

    /// <summary>
    /// 部门层级（如：1级、2级等）
    /// </summary>
    public int DeptLevel { get; set; }

    /// <summary>
    /// 父部门ID（顶级部门为 null 或空字符串）
    /// </summary>
    public string ParentDeptId { get; set; }  = "";

    /// <summary>
    /// 成本中心ID
    /// </summary>
    public string CostCenterId { get; set; } = "";

    /// <summary>
    /// 注销日期（软删除时间）
    /// </summary>
    public DateTime? CancelDate { get; set; }

    /// <summary>
    /// 是否注销（1-是，0-否）
    /// </summary>
    public byte IsCancel { get; set; }

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