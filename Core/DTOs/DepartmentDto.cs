namespace Core.DTOs;

public class DepartmentDto
{
    public string Id { get; set; } = "";
    public string CompanyId { get; set; } = "";
    public string DeptId { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string DeptLevel { get; set; } = "";
    public string ParentDeptId { get; set; }  = "";
    public string CostCenterId { get; set; } = "";
    public string CancelDate { get; set; }  = "";
    public string IsCancel { get; set; } = "";
    public string CreatedBy { get; set; } = "";
    public string CreatedTime { get; set; } = "";
    public string ModifiedBy { get; set; } = "";
    public string ModifiedTime { get; set; } = "";
}