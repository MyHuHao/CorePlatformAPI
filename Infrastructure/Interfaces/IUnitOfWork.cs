using MySql.Data.MySqlClient;

namespace Infrastructure.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    MySqlTransaction? CurrentTransaction { get; }
}