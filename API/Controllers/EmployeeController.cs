using Core.Contracts.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class EmployeeController(IEmployeeService service) : Controller
{
    /// <summary>
    ///     通过用户ID获取详情
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> GetEmployeeById([FromBody] ByEmployeeRequest request)
    {
        var result = await service.GetEmployeeById(request);
        return Ok(result);
    }
    
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> GetEmployeePage([FromBody] ByEmployeeListRequest request)
    {
        var result = await service.GetEmployeePageAsync(request);
        return Ok(result);
    }
}