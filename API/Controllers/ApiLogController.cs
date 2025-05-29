using Core.Contracts.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class ApiLogController(IApiLogService service) : ControllerBase
{
    /// <summary>
    ///     分页查询-获取访问日志
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> GetByApiLogPage([FromBody] ByApiLogListRequest request)
    {
        var result = await service.GetByApiLogPage(request);
        return Ok(result);
    }
}