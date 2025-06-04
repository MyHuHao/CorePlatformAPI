namespace Core.Contracts.Results;

public class AccountResult
{
    public string LoginName { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string EmpId { get; set; } = "";
    public string EmpName { get; set; } = "";
    public byte IsActive { get; set; } 
    public byte AccountType { get; set; }
    public string DeptId { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Language { get; set; } = "";
}