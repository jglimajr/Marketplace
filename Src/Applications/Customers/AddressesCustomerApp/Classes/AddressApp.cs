using System;
using InteliSystem.Utils.Globals.Classes;

namespace InteliSystem.InteliMarketPlace.Applications.AddressesCustomer;

public class AddressApp : ClassBaseAppGuid
{
    public Guid IdCustomer { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
}
