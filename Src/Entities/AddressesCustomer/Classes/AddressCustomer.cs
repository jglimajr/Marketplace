using System;
using System.ComponentModel.DataAnnotations.Schema;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;

namespace InteliSystem.InteliMarketPlace.Entities.AddressesCustomer
{
    [Table("CustomerAddresses")]
    public class AddressCustomer : ClassBaseGuid
    {
        private AddressCustomer()
            : base() { }
        public AddressCustomer(Guid idCustomer, string description, string address, string number, string complement, string neighborhood, string city, string state, string country, string zipCode)
            : this()
        {
            LoadProperties(idCustomer, description, address, number, complement, neighborhood, city, state, country, zipCode);
        }

        public AddressCustomer(Guid id, Guid idCustomer, string description, string address, string number, string complement, string neighborhood,
                               string city, string state, string country, string zipCode, StatusValues status = StatusValues.Active)
            : base(id, status)
        {
            LoadProperties(idCustomer, description, address, number, complement, neighborhood, city, state, country, zipCode);
        }

        private void LoadProperties(Guid idCustomer, string description, string address, string number, string complement, string neighborhood, string city, string state, string country, string zipCode)
        {
            IdCustomer = idCustomer;
            Description = description;
            Address = address;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            Validate();
        }

        private void Validate()
        {
            if (IdCustomer.IsNull())
                this.AddNotification("IdCustomer", "Cliente não informado");
            if (Address.IsEmpty())
                this.AddNotification("Address", "Endereço não informado");
            if (Number.IsEmpty())
                this.AddNotification("Number", "Endereço não informado");
            if (Neighborhood.IsEmpty())
                this.AddNotification("Neighborhood", "Endereço não informado");
            if (City.IsEmpty())
                this.AddNotification("City", "Endereço não informado");
            if (State.IsEmpty())
                this.AddNotification("State", "Endereço não informado");
            if (Country.IsEmpty())
                this.AddNotification("Country", "Endereço não informado");
            if (ZipCode.NumbersOnly().IsEmpty())
                this.AddNotification("ZipCode", "Endereço não informado");
        }
        public Guid IdCustomer { get; private set; }
        public string Description { get; private set; }
        public string Address { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
    }
}