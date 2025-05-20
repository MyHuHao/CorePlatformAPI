using System.Data.Common;

namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    DbTransaction? CurrentTransaction { get; }
    DbConnection? CurrentConnection { get; }
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}