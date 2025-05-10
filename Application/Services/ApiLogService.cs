using Application.Commands;
using Core.Entities;
using Core.Interfaces.Services;

namespace Application.Services;

public class ApiLogService(UserCommand command) : IApiLogService
{
    /// <summary>
    ///     插入api日志
    /// </summary>
    /// <param name="apiLog"></param>
    public async Task InsertApiLog(ApiLog apiLog)
    {
        await command.AddAsync(apiLog);
    }
}