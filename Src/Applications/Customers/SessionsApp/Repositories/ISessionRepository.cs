using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Entities.Sessions;
using InteliSystem.InteliMarketPlace.Repositories;
using InteliSystem.Utils.Globals.Interfaces;

namespace InteliSystem.InteliMarketPlace.Applications.SessionsApp
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        Task<Session> GetRefreshTokenAsync(string idcustomer, string device);
    }
}