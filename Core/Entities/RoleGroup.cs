namespace Core.Entities;

public class RoleGroup
{
    /// <summary>
    ///     唯一标识(UUID)
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    ///     公司ID
    /// </summary>
    public string CompanyId { get; set; } = "";

    /// <summary>
    ///     角色组ID(自增唯一)
    /// </summary>
    public string RoleGroupId { get; set; } = "";

    /// <summary>
    ///     角色组名称
    /// </summary>
    public string RoleGroupName { get; set; } = "";

    /// <summary>
    ///     角色组描述
    /// </summary>
    public string RoleGroupDesc { get; set; } = "";

    /// <summary>
    ///     状态(1启用/0禁用)
    /// </summary>
    public byte Status { get; set; }

    /// <summary>
    ///     创建人
    /// </summary>
    public string CreatedBy { get; set; } = "";

    /// <summary>
    ///     创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    ///     修改人
    /// </summary>
    public string ModifiedBy { get; set; } = "";

    /// <summary>
    ///     修改时间
    /// </summary>
    public DateTime ModifiedTime { get; set; }
}