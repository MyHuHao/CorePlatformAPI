using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DictionaryController(DictionaryService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List() =>
        Ok(await service.GetListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) =>
        Ok(await service.GetAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(DictionaryItem item)
    {
        await service.CreateAsync(item);
        return Ok(new { code = 0, msg = "新增成功" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, DictionaryItem item)
    {
        item.Id = id;
        await service.UpdateAsync(item);
        return Ok(new { code = 0, msg = "更新成功" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await service.DeleteAsync(id);
        return Ok(new { code = 0, msg = "删除成功" });
    }
}