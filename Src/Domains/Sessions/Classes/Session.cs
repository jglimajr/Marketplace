using System;
using System.ComponentModel.DataAnnotations.Schema;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;

namespace InteliSystem.InteliMarketPlace.Domains.Sessions
{
    [Table("Sessions")]
    public class Session : ClassBaseGuid
    {

        private Session()
            : base() { }

        public Session(Guid idcustomer, string device, string token, string refreshtoken)
            : this()
        {
            LoadParam(idcustomer, device, token, refreshtoken);
        }
        public Session(Guid id, Guid idcustomer, string device, string token, string refreshtoken, StatusValues status = StatusValues.Active)
            : base(id: id, status: status)
        {
            LoadParam(idcustomer, device, token, refreshtoken);
        }

        private void LoadParam(Guid idcustomer, string device, string token, string refreshtoken)
        {
            this.IdCustomer = idcustomer;
            this.Device = device;
            this.Token = token;
            this.RefreshToken = refreshtoken;

            Validate();
        }

        private void Validate()
        {
            if (this.Device.IsEmpty())
                this.AddNotification("Session.Device", "SessionDevice");
            if (this.Token.IsEmpty())
                this.AddNotification("Session.Token", "SessionToken");
            if (this.RefreshToken.IsEmpty())
                this.AddNotification("Session.RefreshToken", "SessionRefreshToken");
        }
        public Guid IdCustomer { get; private set; }
        public string Device { get; private set; }
        public string Token { get; private set; }
        public string RefreshToken { get; private set; }
    }
}