using System;
using System.ComponentModel.DataAnnotations.Schema;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;
using Utils.Globals.Classes;

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
            this.EMail = new EMail(email);
            this.PassWord = password.ToSha512($"{this.Id}{password}");
        }
        public Customer(Guid id, string firstname, string email, DateTime? birthdate = null, GenderValues gender = GenderValues.Uninformed,
                        StatusValues status = StatusValues.Active)
            : base(id: id, status: status)
        {
            this.Name = firstname;
            this.BirthDate = birthdate;
            this.Gender = gender;
            this.EMail = new EMail(email);
        }
        public string Name { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public GenderValues Gender { get; private set; }
        public EMail EMail { get; private set; }
        public string PassWord { get; private set; }
    }
}