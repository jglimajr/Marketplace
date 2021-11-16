using System;
using System.ComponentModel.DataAnnotations.Schema;
using InteliSystem.Utils.Dapper.Extensions.Attributes;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;

namespace InteliSystem.InteliMarketPlace.Domains.Customers
{
    [Table("[Customers]")]
    public class Customer : ClassBaseGuid
    {

        private Customer()
            : base() { }

        public Customer(string firstname, string email, DateTime? birthdate = null, GenderValues gender = GenderValues.Uninformed,
                         string password = null)
            : this()
        {
            this.Name = firstname;
            this.BirthDate = birthdate;
            this.Gender = gender;
            this.EMail = email;
            this.PassWord = password.ToSha512($"{this.Id}{password}{EMail}");
            Validate();
        }
        public Customer(Guid id, string firstname, string email, DateTime? birthdate = null, GenderValues gender = GenderValues.Uninformed,
                        StatusValues status = StatusValues.Active)
            : base(id: id, status: status)
        {
            this.Name = firstname;
            this.BirthDate = birthdate;
            this.Gender = gender;
            this.EMail = email;
            Validate();
        }

        private void Validate()
        {
            if (Name.IsEmpty())
                this.AddNotification("Name", "CustNotName");
            if (EMail.IsNotEMail())
                this.AddNotification("E-Mail", "CustNotEmail");
            if (!BirthDate.Between(new DateTime(1900, 1, 1), DateTime.Now))
                this.AddNotification("BirthDate", "CustomerBirthdate");

        }
        public string Name { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public GenderValues Gender { get; private set; }
        public string EMail { get; private set; }
        [UpdateProperty(false)]
        public string PassWord { get; private set; }
    }
}