using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using InteliSystem.Utils.Globals.Classes;

namespace InteliSystem.InteliMarketPlace.Repositories
{
    public class GenericRepository<T> : ClassBaseRepository where T : class
    {
        private readonly IDbConnection _conn;
        public GenericRepository(IDbConnection conn)
            : base(conn) => this._conn = conn;


        public virtual Task<int> AddAsync(T tobject)
            => this.Connection.InsertAsync<T>(tobject);

        public virtual Task<bool> UpdateAsync(T tobject)
            => this.Connection.UpdateAsync<T>(tobject);

        public virtual Task<bool> DeleteAsync(T tobject)
            => this.Connection.DeleteAsync<T>(tobject);

    }
}