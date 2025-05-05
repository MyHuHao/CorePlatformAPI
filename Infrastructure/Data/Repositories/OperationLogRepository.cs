using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data.Repositories;

public class OperationLogRepository : IOperationLogRepository
{
    public async Task InsertAsync(OperationLog log)
    {
        // 具体实现逻辑（例如写入数据库）
        await Task.CompletedTask; // 示例代码
    }
}