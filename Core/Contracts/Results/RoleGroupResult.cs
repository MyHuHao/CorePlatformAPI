namespace Core.Contracts.Results;

public class RoleGroupMenuResult
{
    public string CompanyId { get; set; } = "";
    public string RoleGroupId { get; set; } = "";
    public string WebMenuId { get; set; } = "";
    public string Title { get; set; } = "";
}

public class RoleGroupResResult
{
    public string CompanyId { get; set; } = "";
    public string RoleGroupId { get; set; } = "";
    public string ResId { get; set; } = "";
    public string ResName { get; set; } = "";
    public string WebMenuId { get; set; } = "";
}