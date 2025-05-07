using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public class ApiLogService(IApiLogRepository repository) : IApiLogService
{
    /// <summary>
    /// 插入api日志
    /// </summary>
    /// <param name="apiLog"></param>
    public async Task InsertApiLog(ApiLog apiLog)
    {
        await repository.AddAsync(apiLog);
    }
}