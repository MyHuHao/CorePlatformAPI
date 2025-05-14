using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Commands;

public class ApiLogCommand(IApiLogRepository repository)
{
    public async Task AddAsync(ApiLog apiLog)
    {
        await repository.AddAsync(apiLog);
    }
}