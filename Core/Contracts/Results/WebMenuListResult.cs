namespace Core.Contracts.Results;

public class WebMenuListResult
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
    public List<WebMenuListResult> Children { get; set; } = [];
}

public class ParentWebMenuListResult
{
    public string Value { get; set; } = "";
    public string Label { get; set; } = "";
    public string ParentWebMenuId { get; set; } = "";
    public string Sequence { get; set; } = "";
    public List<ParentWebMenuListResult> Children { get; set; } = [];
}