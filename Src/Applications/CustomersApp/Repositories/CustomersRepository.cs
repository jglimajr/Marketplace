using System;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Domains.Customers;
using InteliSystem.Utils.Globals.Interfaces;

namespace InteliSystem.InteliMarketPlace.Applications.CustomersApp.Repositories
{
    public interface ICustomersRepository : IRepositoryGeneralGuid<Customer>
    {
        Task<int> UpdateEMail(Guid id, string email);
        Task<int> UpdatePassWord(Guid id, string password);
    }
}