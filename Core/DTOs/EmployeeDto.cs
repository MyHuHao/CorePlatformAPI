namespace Core.DTOs;

public class EmployeeDto
{
    public string Id { get; set; } = "";
    public string CompanyId { get; set; } = "";
    public string EmpId { get; set; } = "";
    public string EmpName { get; set; } = "";
    public string EmpMobilePhone { get; set; } = "";
    public string EmpEmail { get; set; } = "";
    public string EmpEntryDate { get; set; } = "";
    public string EmpDepartureDate { get; set; } = "";
    public int UserType { get; set; }
    public string DeptId { get; set; } = "";
    public string CostCenterId { get; set; } = "";
    public string JobCategoryId { get; set; } = "";
    public int Status { get; set; }
    public string Direct { get; set; } = "";
    public string DeliveredDate { get; set; } = "";
    public string Birthday { get; set; } = "";
    public string Sex { get; set; } = "";
    public int WorkYear { get; set; }
    public string EducationName { get; set; } = "";
    public string CreatedBy { get; set; } = "";
    public string CreatedTime { get; set; } = "";
    public string ModifiedBy { get; set; } = "";
    public string ModifiedTime { get; set; } = "";
}