using System.Data.Common;
using Core.Interfaces.Repositories;
using MySql.Data.MySqlClient;

namespace Infrastructure.Data;

public class UnitOfWork(IDbConnectionFactory connectionFactory) : IUnitOfWork
{
    private MySqlConnection? _connection;
    public DbTransaction? CurrentTransaction { get; private set; }

    public async Task BeginTransactionAsync()
    {
        _connection = connectionFactory.CreateConnection() as MySqlConnection;
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