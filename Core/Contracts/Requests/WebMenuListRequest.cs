namespace Core.Contracts.Requests;

public class ByWebMenuListRequest
{
    public string Name { get; set; } = "";
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class AddWebMenuRequest
{
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
    public string StaffId { get; set; } = "";
}

public class UpdateWebMenuRequest : AddWebMenuRequest
{
    public string Id { get; set; } = "";
}

public class VerifyWebMenuRequest
{
    public string Sequence { get; set; } = "";
    public string Name { get; set; } = "";
    public string ParentWebMenuId { get; set; } = "";
}