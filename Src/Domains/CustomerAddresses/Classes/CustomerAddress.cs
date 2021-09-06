using System;
using System.ComponentModel.DataAnnotations.Schema;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;

namespace InteliSystem.InteliMarketPlace.Domains.CustomerAddresses
{
    [Table("[CustomerAddresses]")]
    public class CustomerAddress : ClassBaseGuid
    {

        private CustomerAddress()
            : base() { }
        public CustomerAddress(Guid idCustomer, string description, string address, string number, string complement, string neighborhood, string city, string state, string country, string zipCode)
            : this()
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
        }

        public CustomerAddress(Guid id, Guid idCustomer, string description, string address, string number, string complement, string neighborhood,
                               string city, string state, string country, string zipCode, StatusValues status = StatusValues.Active)
            : base(id, status)
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