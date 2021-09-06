using System;
using System.Threading.Tasks;

namespace InteliSystem.Utils.Globals.Interfaces
{
    public interface IRepositoryGeneralGuid : IDisposable
    {
        Task<int> AddAsync<T>(T tobjct) where T : class;
        Task<bool> UpdateAsync<T>(T tobjct) where T : class;
        Task<bool> DeleteAsync<T>(T tobjct) where T : class;
        Task<T> GetAsync<T>(Guid id) where T : class;
    }
}