using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidator;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Classes;
using InteliSystem.InteliMarketPlace.Applications.CustomersApp.Repositories;
using InteliSystem.InteliMarketPlace.Domains.Customers;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;

namespace InteliSystem.InteliMarketPlace.Applications.CustomersApp
{
    public class CustomersAppMaintenance : Notifiable
    {
        private ICustomersRepository _repository;
        public CustomersAppMaintenance(ICustomersRepository repository)
            : base()
        {
            this._repository = repository;
        }

        public Task<Return> AddAsync(CustomerApp customerapp)
        {
            if (customerapp.IsNull())
            {
                this.AddNotification("Customer", "Customer not informed");
                return Task.Run<Return>(() => { return new Return(ReturnValues.Failed, null); });
            }

            var customer = new Customer(firstname: customerapp.Name, email: customerapp.EMail, password: customerapp.PassWord);

            var retAux = this._repository.AddAsync(customer).GetAwaiter().GetResult();
            if (retAux <= 0)
            {
                this.AddNotification("Customer", "Failure to include customer");
                return Task.Run<Return>(() => { return new Return(ReturnValues.Failed, null); });
            }

            return Task.Run<Return>(() =>
            {
                var customerret = new CustomerProfile();
                customerret.Load(customer);
                return new Return(ReturnValues.Success, customerret);
            });
        }

        public Task<Return> UpdateAsync(string id, CustomerProfile customer)
        {
            return Task.Run<Return>(() =>
            {
                if (new Guid(id) != customer.Id)
                {
                    this.AddNotification("Customer", "Customer invalid");
                    return new Return(ReturnValues.Failed, null);
                }

                var customeraux = this._repository.GetAsync(customer.Id).GetAwaiter().GetResult();
                customeraux.Load(customer);
                // var customeraux = new Customer(id: customer.Id, firstname: customer.Name, birthdate: customer.BirthDate.ToDateTimeOrNull(),
                //                                 gender: customer.Gender, email: customer.EMail);

                var retAux = this._repository.UpdateAsync(customeraux).GetAwaiter().GetResult();

                if (retAux == false)
                {
                    this.AddNotification("Customer", "Failure to update customer profile");
                    return new Return(ReturnValues.Failed, null);
                }

                return new Return(ReturnValues.Success, new CustomerProfile().Load(customeraux));

            });
        }

        public Task<Return> GetAsyn(string id = null)
        {
            if (id.IsEmpty())
                return this.GetAllAsync();

            var objAux = this._repository.GetAsync(new Guid(id)).GetAwaiter().GetResult();
            if (objAux.IsNull())
            {
                this.AddNotification("Customer", "Customer not found");
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
            var objAux = this._repository.GetAllAsync().GetAwaiter().GetResult();

            if (objAux.ToList().Count <= 0)
            {
                this.AddNotification("Customers", "Customers not found");
                return Task.Run<Return>(() => { return new Return(ReturnValues.Failed, null); });
            }

            return Task.Run<Return>(() =>
            {
                IList<CustomerProfile> customers = new List<CustomerProfile>();
                objAux.OrderBy(c => c.Name).ToList().ForEach(customer =>
                {
                    var customerapp = new CustomerProfile();
                    customers.Add(customerapp.Load(customer));
                });

                return new Return(ReturnValues.Success, customers);
            });
        }
    }
}