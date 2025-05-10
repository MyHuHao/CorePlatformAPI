using System.Data;
using System.Data.Common;
using Core.Interfaces.Repositories;

namespace Infrastructure.Data;

public class UnitOfWork(IDbConnectionFactory connectionFactory) : IUnitOfWork
{
    public DbTransaction? CurrentTransaction { get; private set; }
    public DbConnection? CurrentConnection { get; private set; }

    public async Task BeginTransactionAsync()
    {
        CurrentConnection ??= connectionFactory.CreateConnection();
        if (CurrentConnection.State != ConnectionState.Open)
        {
            await CurrentConnection.OpenAsync();
        }

        CurrentTransaction = await CurrentConnection.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (CurrentTransaction == null) throw new InvalidOperationException("事务未启动");
        await CurrentTransaction.CommitAsync();
        await DisposeAsync();
    }

    public async Task RollbackAsync()
    {
        if (CurrentTransaction == null) return;
        await CurrentTransaction.RollbackAsync();
        await DisposeAsync();
    }

    public void Dispose()
    {
        DisposeAsync().GetAwaiter().GetResult();
        GC.SuppressFinalize(this);
    }

    private async Task DisposeAsync()
    {
        if (CurrentTransaction != null)
        {
            await CurrentTransaction.DisposeAsync();
            CurrentTransaction = null;
        }

        if (CurrentConnection != null)
        {
            await CurrentConnection.DisposeAsync();
            CurrentConnection = null;
        }
    }
}