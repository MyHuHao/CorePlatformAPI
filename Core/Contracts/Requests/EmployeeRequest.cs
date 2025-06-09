namespace Core.Contracts.Requests;

public class ByEmployeeRequest
{
    public required string CompanyId { get; set; }
    public required string EmpId { get; set; }
}

public class ByEmployeeListRequest
{
    public required string CompanyId { get; set; }
    public string EmpId { get; set; } = "";
    public string EmpName { get; set; } = "";
    public string Status { get; set; } = "";
    public required int Page { get; set; } = 1;
    public required int PageSize { get; set; } = 20;
}

public class AddEmployeeRequest
{
    public required string CompanyId { get; set; }
    public required string EmpId { get; set; } = "";
    public required string EmpName { get; set; } = "";
    public string EmpMobilePhone { get; set; } = "";
    public string EmpEmail { get; set; } = "";
    public string Birthday { get; set; } = "";
    public string Sex { get; set; } = "";
    public string UserType { get; set; } = "";
    public string Direct { get; set; } = "";
    public string DeptId { get; set; } = "";
    public string CostCenterId { get; set; } = "";
    public string JobCategoryId { get; set; } = "";
    public string EducationName { get; set; } = "";
    public string DeliveredDate { get; set; } = "";
    public string EmpEntryDate { get; set; } = "";
    public string EmpDepartureDate { get; set; } = "";
    public string WorkYear { get; set; } = "";
    public string Status { get; set; } = "";
    public string StaffId { get; set; } = "";
}

public class UpdateEmployeeRequest
{
}