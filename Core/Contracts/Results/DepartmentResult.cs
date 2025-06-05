namespace Core.Contracts.Results;

public class TreeDepartmentResult
{
    public string Id { get; set; } = "";
    public string CompanyId { get; set; } = "";
    public string DeptId { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string DeptLevel { get; set; } = "";
    public string ParentDeptId { get; set; }  = "";
    public List<TreeDepartmentResult> Children { get; set; } = [];
}