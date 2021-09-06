using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace InteliSystem.Utils.Globals.Interfaces
{
    public interface IConnection : IDbConnection, IDisposable
    {
        Task OpenAsync();
        Task CloseAsync();
        ValueTask DisposeAsync();
        Task ChangeDatabaseAsync(string databaseName);
        ValueTask<DbTransaction> BeginTransactionAsync();
        ValueTask<DbTransaction> BeginTransactionAsync(IsolationLevel il);
    }
}