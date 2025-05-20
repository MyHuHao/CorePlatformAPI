using Core.Contracts.Requests;
using Core.Interfaces.Repositories;

namespace Application.Commands;

public class ApiLogCommand(IApiLogRepository repository)
{
    /// <summary>
    /// 插入api日志
    /// </summary>
    /// <param name="apiLog"></param>
    public async Task AddAsync(AddApiLogRequest apiLog)
    {
        await repository.AddApiLogAsync(apiLog);
    }
}