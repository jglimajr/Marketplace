using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Classes;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Repositories;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;
using InteliSystem.InteliMarketPlace.Domains.Customers;
using Utils.Globals.Notifications;

namespace InteliSystem.InteliMarketPlace.Applications.CustomersApp
{
    public class CustomersAppMaintenance : InteliNotification
    {
        private readonly ICustomersRepository _repository;
        public CustomersAppMaintenance(ICustomersRepository repository)
            : base()
        {
            this._repository = repository;
        }

        public Task<Return> AddAsync(CustomerApp customerapp)
        {


            return Task.Run<Return>(() =>
            {
                if (customerapp.IsNull())
                {
                    this.AddNotification("Customer", "CustNotInf");
                    return new Return(ReturnValues.Failed, null);
                }

                var customer = new Customer(firstname: customerapp.Name, email: customerapp.EMail, password: customerapp.PassWord);

                if (customer.ExistNotifications)
                {
                    this.AddNotifications(customer.GetAllNotifications);
                    return new Return(ReturnValues.Failed, null);
                }

                var retAux = this._repository.AddAsync(customer).GetAwaiter().GetResult();
                if (retAux <= 0)
                {
                    this.AddNotification("Customer", "CustFailAdd");
                    return new Return(ReturnValues.Failed, null);
                }
                var customerret = new CustomerProfile().Load(customer);

                return new Return(ReturnValues.Success, customerret);
            });
        }

        public Task<Return> UpdateAsync(string id, CustomerProfile customer)
        {
            return Task.Run<Return>(() =>
            {
                if (new Guid(id) != customer.Id)
                {
                    this.AddNotification("Customer", "CustNotInf");
                    return new Return(ReturnValues.Failed, null);
                }

                var customeraux = this._repository.GetAsync(new Customer(customer.Id, "", "")).GetAwaiter().GetResult();

                if (customeraux.IsNull())
                {
                    this.AddNotification("Customer", "CustNotFound");
                    return new Return(ReturnValues.Failed, null);
                }
                customeraux.Load(customer);

                var customerUpdate = new Customer(id: customeraux.Id, firstname: customeraux.Name, birthdate: customeraux.BirthDate,
                                                gender: customeraux.Gender, email: customeraux.EMail);
                if (customerUpdate.ExistNotifications)
                {
                    this.AddNotifications(customerUpdate.GetAllNotifications);
                    return new Return(ReturnValues.Failed, null);
                }

                var retAux = this._repository.UpdateAsync(customerUpdate).GetAwaiter().GetResult();

                if (retAux <= 0)
                {
                    this.AddNotification("Customer", "CustFailUpdate");
                    return new Return(ReturnValues.Failed, null);
                }

                return new Return(ReturnValues.Success, new CustomerProfile().Load(customeraux));

            });
        }

        public Task<Return> GetAsync(string id)
        {
            if (id.IsEmpty())
                return this.GetAllAsync();

            var objAux = this._repository.GetAsync(new Customer(new Guid(id), "", "")).GetAwaiter().GetResult();
            if (objAux.IsNull())
            {
                this.AddNotification("Customer", "CustNotFound");
                return Task.Run<Return>(() => { return new Return(ReturnValues.Failed, null); });
            }

            return Task.Run<Return>(() =>
            {
                var customerapp = new CustomerProfile().Load(objAux);
                return new Return(ReturnValues.Success, customerapp);
            });
        }

        public Task<Return> GetAllAsync()
        {

            return Task.Run<Return>(() =>
            {
                var objAux = this._repository.GetAllAsync().GetAwaiter().GetResult();

                IList<CustomerProfile> customers = new List<CustomerProfile>();
                objAux.OrderBy(c => c.Name).ToList().ForEach(customer =>
                {
                    var customerapp = new CustomerProfile();
                    customers.Add(customerapp.Load(customer));
                });

                return new Return(ReturnValues.Success, customers);
            });
        }

        public Task<Return> GetByEMailAsync(string email)
        {
            return Task.Run<Return>(() =>
            {
                var objAux = this._repository.GetByEMailAsync(email).GetAwaiter().GetResult();
                if (objAux.IsNull())
                {
                    this.AddNotification("Customer", "CustNotFound");
                    return new Return(ReturnValues.Failed, null);
                }

                return new Return(ReturnValues.Success, new CustomerProfile().Load(objAux));

            });
        }
    }
}