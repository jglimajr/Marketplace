using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using InteliSystem.Utils.Globals.Interfaces;
using Microsoft.Data.SqlClient;

namespace InteliSystem.Connection
{
    public class Connection : IConnection
    {

        private SqlConnection _conn;

        public Connection(string sqlconnectionstring)
        {
            this._conn = new SqlConnection(sqlconnectionstring);
        }
        public string ConnectionString
        {
            get => this._conn.ConnectionString;
            set => this._conn.ConnectionString = value;
        }

        public int ConnectionTimeout => this._conn.ConnectionTimeout;

        public string Database => this._conn.Database;

        public ConnectionState State => this._conn.State;
        public IDbTransaction BeginTransaction()
        {
            return this._conn.BeginTransaction();

        }
        public ValueTask<DbTransaction> BeginTransactionAsync()
        {
            return this._conn.BeginTransactionAsync();
        }
        public ValueTask<DbTransaction> BeginTransactionAsync(IsolationLevel il)
        {
            return this._conn.BeginTransactionAsync(il);
        }
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return this._conn.BeginTransaction(il);
        }
        public void ChangeDatabase(string databaseName)
        {
            this._conn.ChangeDatabase(databaseName);
        }

        public Task ChangeDatabaseAsync(string databaseName)
        {
            return this._conn.ChangeDatabaseAsync(databaseName);
        }

        public void Close()
        {
            this._conn.Close();

        }
        public Task CloseAsync()
        {
            return this._conn.CloseAsync();
        }

        public IDbCommand CreateCommand()
        {
            return this._conn.CreateCommand();
        }

        public void Dispose()
        {
            this._conn.Dispose();

        }

        public ValueTask DisposeAsync()
        {
            return this._conn.DisposeAsync();
        }

        public void Open()
        {
            this._conn.Open();
        }
        public Task OpenAsync()
        {
            return this._conn.OpenAsync();
        }
    }
}