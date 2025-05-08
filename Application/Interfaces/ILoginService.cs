using Core.Contracts.Requests;
using Core.Contracts.Results;

namespace Application.Interfaces;

public interface ILoginService
{
    Task<ApiResults<string>> Login(LoginRequest request);
}