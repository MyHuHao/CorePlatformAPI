namespace Core.DTOs;

public class WebMenuDto
{
    public string Id { get; set; } = "";
    public string WebMenuId { get; set; } = "";
    public string ParentWebMenuId { get; set; } = "";
    public string Name { get; set; } = "";
    public string Path { get; set; } = "";
    public string Component { get; set; } = "";
    public string Title { get; set; } = "";
    public string Icon { get; set; } = "";
    public string Redirect { get; set; } = "";
    public string Sequence { get; set; } = "";
    public string IsFrame { get; set; } = "";
    public string FrameSrc { get; set; } = "";
    public string IsCache { get; set; } = "";
    public string IsVisible { get; set; } = "";
    public string Permission { get; set; } = "";
    public string MenuType { get; set; } = "";
    public string Status { get; set; } = "";
    public string Remark { get; set; } = "";
}