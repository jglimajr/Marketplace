using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InteliSystem.InteliMarketPlace.Repositories
{
    public interface IGenericRepository<T> : IBaseRepository where T : class
    {
        Task<int> AddAsync(T tobject);
        Task<int> UpdateAsync(T tobject);
        Task<int> DeleteAsync(T tobject);
        Task<T> GetAsync(T tobject);
        Task<IEnumerable<T>> GetAllAsync(dynamic filter = null);
    }
}