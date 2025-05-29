namespace Core.Entities;

public class WebMenu
{
    /// <summary>
    ///     菜单唯一标识（UUID）
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    ///     菜单ID（UUID）
    /// </summary>
    public string WebMenuId { get; set; } = "";

    /// <summary>
    ///     父级菜单ID（顶级菜单为0或null）
    /// </summary>
    public string ParentWebMenuId { get; set; } = "";

    /// <summary>
    ///     菜单名称（唯一）
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    ///     路由路径（唯一）
    /// </summary>
    public string Path { get; set; } = "";

    /// <summary>
    ///     组件路径（如: Layout 或具体组件路径）
    /// </summary>
    public string Component { get; set; } = "";

    /// <summary>
    ///     菜单标题（显示名称）
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    ///     菜单图标（支持 iconify/el-icon 等）
    /// </summary>
    public string Icon { get; set; } = "";

    /// <summary>
    ///     重定向路径
    /// </summary>
    public string Redirect { get; set; } = "";

    /// <summary>
    ///     显示顺序（从小到大排序）
    /// </summary>
    public int Sequence { get; set; }

    /// <summary>
    ///     是否外链（1-是，0-否）
    /// </summary>
    public byte IsFrame { get; set; }

    /// <summary>
    ///     外链地址（当 IsFrame=1 时有效）
    /// </summary>
    public string FrameSrc { get; set; } = "";

    /// <summary>
    ///     是否缓存（1-是，0-否）
    /// </summary>
    public byte IsCache { get; set; }

    /// <summary>
    ///     是否显示（1-是，0-否）
    /// </summary>
    public byte IsVisible { get; set; }

    /// <summary>
    ///     权限标识（如：system:user:list）
    /// </summary>
    public string Permission { get; set; } = "";

    /// <summary>
    ///     菜单类型（M:目录 / C:菜单 / F:按钮）
    /// </summary>
    public string MenuType { get; set; } = "";

    /// <summary>
    ///     状态（1-正常，0-停用）
    /// </summary>
    public byte Status { get; set; }

    /// <summary>
    ///     备注
    /// </summary>
    public string Remark { get; set; } = "";

    /// <summary>
    ///     创建人AccountId
    /// </summary>
    public string CreatedBy { get; set; } = "";

    /// <summary>
    ///     创建时间
    /// </summary>
    public DateTime? CreatedTime { get; set; }

    /// <summary>
    ///     修改人AccountId
    /// </summary>
    public string ModifiedBy { get; set; } = "";

    /// <summary>
    ///     修改时间
    /// </summary>
    public DateTime? ModifiedTime { get; set; }
}