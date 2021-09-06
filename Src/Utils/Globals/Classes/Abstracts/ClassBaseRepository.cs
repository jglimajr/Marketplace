using System;
using System.Data;

namespace InteliSystem.Utils.Globals.Classes
{
    public abstract class ClassBaseRepository : IDisposable
    {
        private IDbConnection _conn;
        public ClassBaseRepository(IDbConnection conn) => this._conn = conn;

        public IDbConnection Connection => this._conn;

        public virtual void Dispose()
        {
            if (this._conn == null)
                return;
            this._conn.Dispose();
        }
    }
}