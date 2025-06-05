using Core.DTOs;

namespace Core.Contracts.Results;

public class LoginResult
{
    public string Token { get; set; } = "";
    public EmployeeDto Employee { get; set; } = new();
}

public class LoginTypeResult
{
    public string Label { get; set; } = "";
    public int Value { get; set; }
}