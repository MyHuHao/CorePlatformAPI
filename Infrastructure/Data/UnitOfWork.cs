using Infrastructure.Interfaces;
using MySql.Data.MySqlClient;

namespace Infrastructure.Data;

public class UnitOfWork(IMySqlConnectionFactory connectionFactory) : IUnitOfWork
{
    private MySqlConnection? _connection;
    public MySqlTransaction? CurrentTransaction { get; private set; }

    public async Task BeginTransactionAsync()
    {
        _connection = connectionFactory.CreateConnection();
        await _connection.OpenAsync();
        CurrentTransaction = await _connection.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (CurrentTransaction == null) throw new InvalidOperationException("No active transaction");
        await CurrentTransaction.CommitAsync();
        Dispose();
    }

    public async Task RollbackAsync()
    {
        if (CurrentTransaction == null) return;
        await CurrentTransaction.RollbackAsync();
        Dispose();
    }

    public void Dispose()
    {
        CurrentTransaction?.Dispose();
        _connection?.Dispose();
    }
}