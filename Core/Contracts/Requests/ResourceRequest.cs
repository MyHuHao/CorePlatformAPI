namespace Core.Contracts.Requests;

public class ByResourceListRequest
{
    public string CompanyId { get; set; } = "";
    public string WebMenuId { get; set; } = "";
    public string ResName { get; set; } = "";
    public string ResType { get; set; } = "";
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class AddResourceRequest
{
    public string CompanyId { get; set; } = "";
    public string ResCode { get; set; } = "";
    public string ResName { get; set; } = "";
    public string ResDesc { get; set; } = "";
    public string ResType { get; set; } = "";
    public int ResSequence { get; set; }
    public string ResPath { get; set; } = "";
    public string WebMenuId { get; set; } = "";
    public string StaffId { get; set; } = "";
}

public class UpdateResourceRequest
{
    public string CompanyId { get; set; } = "";
    public string WebMenuId { get; set; } = "";
    public string Id { get; set; } = "";
    public string ResCode { get; set; } = "";
    public string ResName { get; set; } = "";
    public string ResDesc { get; set; } = "";
    public string ResType { get; set; } = "";
    public int ResSequence { get; set; }
    public string ResPath { get; set; } = "";
    public string StaffId { get; set; } = "";
}

public class ValidResourceCodeRequest
{
    public string CompanyId { get; set; } = "";
    public string WebMenuId { get; set; } = "";
    public string ResCode { get; set; } = "";
}