using System;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;

namespace InteliSystem.InteliMarketPlace.Domains.Sessions
{
    public class Session : ClassBaseGuid
    {
        private Session()
            : base() { }

        public Session(Guid idcustomer, string device)
        {
            this.IdCustomer = idcustomer;
            this.Device = device;
        }
        public Session(Guid id, Guid idcustomer, string device, StatusValues status = StatusValues.Active)
            : base(id: id, status: status)
        {
            this.IdCustomer = idcustomer;
            this.Device = device;
        }

        private void Validate()
        {
            if (this.Device.IsEmpty())
                this.AddNotification("Session.Device", "Device not informed");
        }
        public Guid IdCustomer { get; private set; }
        public string Device { get; private set; }
    }
}