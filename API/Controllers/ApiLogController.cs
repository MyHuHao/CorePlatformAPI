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
    /// 分页查询-获取访问日志
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<IActionResult> GetApiLogByPage([FromBody] ApiLogRequest request)
    {
        var result = await service.GetApiLogByPage(request);
        return Ok(result);
    }
}