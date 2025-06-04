using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;

namespace Core.Interfaces.Services;

public interface IEmployeeService
{
    /// <summary>
    ///     通过用户ID获取详情
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResult<EmployeeDto>> GetEmployeeById(ByEmployeeRequest request);

    /// <summary>
    ///     验证是否有该人员
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<bool> VerifyEmployeeAsync(ByEmployeeRequest request);

    /// <summary>
    ///     分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResult<PagedResult<EmployeeDto>>> GetEmployeePageAsync(ByEmployeeListRequest request);
    
    // 人员选择分页查询
    Task<ApiResult<PagedResult<EmployeeChangeResult>>> GetEmployeePageBySelectAsync(ByEmployeeListRequest request);
}