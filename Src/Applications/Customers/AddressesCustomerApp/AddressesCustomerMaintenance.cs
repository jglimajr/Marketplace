using InteliSystem.Utils.Globals.Classes.Abstracts;

namespace InteliSystem.InteliMarketPlace.Applications.AddressesCustomer
{
    public class AddressesCustomerMaintenance : ClassBaseMaintenance<AddressApp>
    {
        private readonly IAddressCustomerRepository _repository;
        public AddressesCustomerMaintenance(IAddressCustomerRepository repository)
            : base(repository)
        {

        }
    }
}