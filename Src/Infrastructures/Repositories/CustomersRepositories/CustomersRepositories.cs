using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Repositories;
using InteliSystem.InteliMarketPlace.Domains.Customers;
using Utils.Globals.Classes;

namespace InteliSystem.InteliMarketPlace.Repositories.CustomersRepositories
{
    public class CustomersRepositories : GenericRepository<Customer>, ICustomersRepository
    {
        private const string SqlSelect = @"Select Id, Name, BirthDate, Gender, SocialLan, SocialId, Status, DateTimeCreate, DateTimeUpdate, EMail as Value From Customers {0};";
        public CustomersRepositories(IDbConnection conn)
            : base(conn) { }

        public override Task<int> AddAsync(Customer customer)
        {
            var sSql = @"Insert into Customers (Id, Name, BirthDate, Gender, EMail, PassWord, Status, DateTimeCreate) values (@Id, @Name, @BirthDate, @Gender, @EMail, @PassWord, @Status, @DateTimeCreate)";
            var param = new
            {
                customer.Id,
                customer.Name,
                customer.BirthDate,
                customer.Gender,
                customer.EMail,
                customer.PassWord,
                customer.Status,
                customer.DateTimeCreate
            };

            return this.Connection.ExecuteAsync(sSql, param);
        }
        public Task<int> UpdatePassWord(Guid id, string password)
        {
            var sSql = @"Update Customers Set PassWord = @PassWord Where Id = @id";

            return this.Connection.ExecuteAsync(sSql, new { id, password });
        }

        public Task<int> UpdateEMail(Guid id, string email)
        {
            var sSql = @"Update Customers Set EMail = @EMail Where Id = @id";

            return this.Connection.ExecuteAsync(sSql, new { id, email });
        }


        public Task<Customer> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllAsync() => this.Connection.GetAllAsync<Customer>();
    }
}