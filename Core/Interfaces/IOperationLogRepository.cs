using Core.Entities;

namespace Core.Interfaces;

public interface IOperationLogRepository
{
    Task InsertAsync(OperationLog log);
}