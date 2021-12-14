using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Entities.Customers;
using InteliSystem.InteliMarketPlace.Repositories;
using InteliSystem.Utils.Globals.Interfaces;

namespace InteliSystem.InteliMarketPlace.Applications.CustomersApp.Repositories
{
    public interface ICustomersRepository : IGenericRepository<Customer>
    {
        Task<int> UpdateEMailAsync(Guid id, string email);
        Task<int> UpdatePassWordAsync(Guid id, string password);
        Task<Customer> GetByEMailAsync(string email);
    }
}