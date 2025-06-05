namespace Core.Entities;

public class CostCenter
{
    /// <summary>
    /// ID（唯一标识，支持UUID）
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// 公司ID
    /// </summary>
    public string CompanyId { get; set; } = "";

    /// <summary>
    /// 成本中心ID
    /// </summary>
    public string CostCenterId { get; set; } = "";

    /// <summary>
    /// 成本中心名称
    /// </summary>
    public string CostCenterName { get; set; } = "";

    /// <summary>
    /// 状态：1-启用，0-禁用
    /// </summary>
    public byte Status { get; set; }

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