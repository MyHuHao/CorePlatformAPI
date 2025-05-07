using Core.DTOs;
using Core.DTOs.Base;
using Core.Entities;

namespace Application.Interfaces;

public interface ILoginService
{
    Task<ApiResponse<string>> Login(LoginRequest request);
}