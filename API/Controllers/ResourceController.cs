using Core.Contracts.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class ResourceController(IResourceService service) : Controller
{
    /// <summary>
    /// 分页查询-获取资源列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> GetResourceByPage([FromBody] ByResourceListRequest request)
    {
        var result = await service.GetResourceByPageAsync(request);
        return Ok(result);
    }

    // 添加资源
    [HttpPost]
    public async Task<IActionResult> AddResource([FromBody] AddResourceRequest request)
    {
        var result = await service.AddResourceAsync(request);
        return Ok(result);
    }

    // 修改资源
    [HttpPost]
    public async Task<IActionResult> UpdateResource([FromBody] UpdateResourceRequest request)
    {
        var result = await service.UpdateResourceAsync(request);
        return Ok(result);
    }

    // 删除资源
    [HttpDelete]
    public async Task<IActionResult> DeleteResourceById(string id,string companyId)
    {
        var result = await service.DeleteResourceByIdAsync(id,companyId);
        return Ok(result);
    }

    // 通过ID查询资源详细
    [HttpGet("{id}")]
    public async Task<IActionResult> GetResourceById(string id)
    {
        var result = await service.GetResourceByIdAsync(id);
        return Ok(result);
    }
}