using System.Collections.Generic;
using System;
using System.Collections;
using System.ComponentModel;
using System.Threading.Tasks;
using InteliSystem.InteliMarketPlace.Domains.AddressesCustomer;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Classes.Abstracts;
using InteliSystem.Utils.Globals.Enumerators;
using System.Linq;

namespace InteliSystem.InteliMarketPlace.Applications.AddressesCustomer
{
    public class AddressesCustomerAppMaintenance : ClassBaseMaintenance<AddressApp>
    {
        private readonly IAddressCustomerRepository _repository;
        public AddressesCustomerAppMaintenance(IAddressCustomerRepository repository)
            : base(repository)
        {
            this._repository = repository;
        }

        public Task<Return> AddAsync(AddressApp addressapp)
        {
            return Task.Run<Return>(() =>
            {
                if (addressapp.IsNull())
                {
                    return new Return(ReturnValues.Failed, null);
                }

                var address = new AddressCustomer(addressapp.IdCustomer, addressapp.Description, addressapp.Address, addressapp.Number, addressapp.Complement, addressapp.Neighborhood, addressapp.City, addressapp.State, addressapp.Country, addressapp.ZipCode);

                if (address.ExistNotifications)
                {
                    this.AddNotifications(address.GetAllNotifications);
                    return new Return(ReturnValues.Failed, null);
                }

                var retaux = this._repository.AddAsync(address).GetAwaiter().GetResult();

                if (retaux <= 0)
                {
                    return new Return(ReturnValues.Failed, null);
                }

                return new Return(ReturnValues.Success, new AddressApp().Load(address));
            });
        }

        public Task<Return> UpdateAsync(string id, AddressApp addressapp)
        {
            return Task.Run<Return>(() =>
            {

                if (id != addressapp?.Id.ObjectToString())
                {
                    return new Return(ReturnValues.Failed, null);
                }

                var address = new AddressCustomer(addressapp.Id, addressapp.IdCustomer, addressapp.Description, addressapp.Address, addressapp.Number, addressapp.Complement, addressapp.Neighborhood, addressapp.City, addressapp.State, addressapp.Country, addressapp.ZipCode);

                if (address.ExistNotifications)
                {
                    this.AddNotifications(address.GetAllNotifications);
                    return new Return(ReturnValues.Failed, null);
                }

                var retaux = this._repository.UpdateAsync(address).GetAwaiter().GetResult();

                if (retaux <= 0)
                {
                    return new Return(ReturnValues.Failed, null);
                }

                return new Return(ReturnValues.Success, new AddressApp().Load(address));

            });
        }

        public Task<Return> GetAllAsync(Guid idcustomer)
        {
            return Task.Run<Return>(() =>
            {
                var addresses = this._repository.GetAllAsync(idcustomer).GetAwaiter().GetResult();

                if (addresses.ToList().Count <= 0)
                {
                    return new Return(ReturnValues.Failed, null);
                }

                var addressesapp = new List<AddressApp>();

                addresses.ToList().ForEach(address =>
                {
                    addressesapp.Add(new AddressApp().Load(address));
                });

                return new Return(ReturnValues.Success, addressesapp);

            });
        }

        public Task<Return> GetAsync(Guid id)
        {
            return Task.Run<Return>(() =>
            {
                var address = this._repository.GetAsync(new AddressCustomer(id, Guid.NewGuid(), "", "", "", "", "", "", "", "", ""));

                if (address.IsNull())
                {
                    return new Return(ReturnValues.Failed, null);
                }

                return new Return(ReturnValues.Success, new AddressApp().Load(address));
            });
        }

    }
}