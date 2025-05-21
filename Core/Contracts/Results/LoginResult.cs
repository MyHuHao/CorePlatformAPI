using Core.DTOs;

namespace Core.Contracts.Results;

public class LoginResult
{
    public string Token { get; set; } = "";
    public EmployeeDto Employee { get; set; } = new();
}