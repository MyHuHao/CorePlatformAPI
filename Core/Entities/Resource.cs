namespace Core.Entities;

public class Resource
{
    /// <summary>
    /// 唯一标识（UUID）
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// 公司ID
    /// </summary>
    public string CompanyId { get; set; } = "";

    /// <summary>
    /// 资源权限标记（如：system:user:list）
    /// </summary>
    public string ResCode { get; set; } = "";

    /// <summary>
    /// 资源名称
    /// </summary>
    public string ResName { get; set; } = "";

    /// <summary>
    /// 资源描述
    /// </summary>
    public string ResDesc { get; set; } = "";

    /// <summary>
    /// 资源类型（如：菜单、按钮、接口）
    /// </summary>
    public string ResType { get; set; } = "";

    /// <summary>
    /// 显示顺序
    /// </summary>
    public int ResSequence { get; set; }

    /// <summary>
    /// 资源路径/标识（如 API路径、前端路由等）
    /// </summary>
    public string ResPath { get; set; } = "";

    /// <summary>
    /// 关联菜单ID（可为空）
    /// </summary>
    public string WebMenuId { get; set; } = "";

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