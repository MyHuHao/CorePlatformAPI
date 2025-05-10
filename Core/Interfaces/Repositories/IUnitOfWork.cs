using System.Data.Common;

namespace Core.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    DbTransaction? CurrentTransaction { get; }
}