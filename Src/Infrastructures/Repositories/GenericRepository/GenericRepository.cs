using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using InteliSystem.Utils.Dapper.Extensions;

namespace InteliSystem.InteliMarketPlace.Repositories
{
    public class GenericRepository<T> : BaseRepository, IGenericRepository<T> where T : class
    {
        private readonly IDbConnection _conn;

        public GenericRepository(IDbConnection conn)
            : base(conn) => this._conn = conn;


        public virtual Task<int> AddAsync(T tobject)
            => this.Connection.InsertAsync<T>(tobject);

        public virtual Task<int> UpdateAsync(T tobject)
            => this.Connection.UpdateAsync<T>(tobject);

        public virtual Task<int> DeleteAsync(T tobject)
            => this.Connection.DeleteAsync<T>(tobject);

        public virtual Task<IEnumerable<T>> GetAllAsync(dynamic filter = null)
            => this.Connection.GetAllAsync<T>();

        public virtual Task<T> GetAsync(T tobjct)
            => this.Connection.GetAsync<T>(tobjct);

    }
}