using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Repositories;
using InteliSystem.InteliMarketPlace.Domains.Customers;

namespace InteliSystem.InteliMarketPlace.Repositories.CustomersRepositories
{
    public class CustomersRepositories : GenericRepository<Customer>, ICustomersRepository
    {
        private const string SqlSelect = @"Select Id, Name, BirthDate, Gender, EMail, PassWord, Status, DateTimeCreate, DateTimeUpdate From Customers {0};";
        public CustomersRepositories(IDbConnection conn)
            : base(conn) { }

        public Task<Customer> GetByEMailAsync(string email)
        {
            var sSql = string.Format(SqlSelect, "Where EMail = @EMail");

            return this.Connection.QueryFirstOrDefaultAsync<Customer>(sSql, new { EMail = email });
        }

        public Task<int> UpdateEMailAsync(Guid id, string email)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePassWordAsync(Guid id, string password)
        {
            throw new NotImplementedException();
        }

    }
}