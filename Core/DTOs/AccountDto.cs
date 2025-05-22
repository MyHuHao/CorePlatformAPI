namespace Core.DTOs;

public class AccountDto
{
    public string Id { get; set; } = "";
    public string CompanyId { get; set; } = "";
    public string LoginName { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string EmpId { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string PasswordSalt { get; set; } = "";
    public string AccountType { get; set; } = "";
    public string IsActive { get; set; } = "";
    public string DeptId { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Language { get; set; } = "";
    public string LastLoginTime { get; set; } = "";
    public string LastLoginIp { get; set; } = "";
    public string FailedLoginAttempts { get; set; } = "";
    public string IsLocked { get; set; } = "";
}