namespace Core.Contracts.Requests;

public class ByEmployeeRequest
{
    public required string CompanyId { get; set; }
    public required string EmpId { get; set; }
}

public class ByEmployeeListRequest
{
    public required string CompanyId { get; set; }
    public required string EmpId { get; set; }
    public required int Page { get; set; } = 1;
    public required int PageSize { get; set; } = 20;
}

public class AddEmployeeRequest
{
    public required int CompanyId { get; set; }
    public required string EmpId { get; set; } = "";
    public required string EmpName { get; set; } = "";
    public string EmpMobilePhone { get; set; } = "";
    public string EmpEmail { get; set; } = "";
    public DateTime? EmpEntryDate { get; set; }
    public DateTime? EmpDepartureDate { get; set; }
    public required int UserType { get; set; }
    public string DeptId { get; set; } = "";
    public string CostCenterId { get; set; } = "";
    public string JobCategoryId { get; set; } = "";
    public int Status { get; set; }
    public string Direct { get; set; } = "";
    public DateTime? DeliveredDate { get; set; }
    public DateTime? Birthday { get; set; }  
    public string Sex { get; set; } = "";
    public int WorkYear { get; set; }
    public string EducationName { get; set; } = "";
    public required string StaffId { get; set; } = "";
}