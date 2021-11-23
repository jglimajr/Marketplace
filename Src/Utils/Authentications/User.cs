using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InteliSystem.Utils.Authentications
{
    public class User
    {
        public string Id { get; set; }
        public string IdCustomer { get; set; }
        public string Device { get; set; }
        public DateTime DateTimeCreate { get; set; }
    }
}