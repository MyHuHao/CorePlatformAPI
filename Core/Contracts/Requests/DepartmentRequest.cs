namespace Core.Contracts.Requests;

public class ByDepartmentListRequest
{
    public string CompanyId { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string IsCancel { get; set; } = "";
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}