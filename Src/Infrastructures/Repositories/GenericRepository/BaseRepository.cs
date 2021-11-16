using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace InteliSystem.InteliMarketPlace.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly IDbConnection _conn;
        protected BaseRepository(IDbConnection conn) => _conn = conn;
        public IDbConnection Connection => this._conn;

        public void Dispose() => this._conn?.Dispose();

    }
}