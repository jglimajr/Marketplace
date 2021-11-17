using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteliSystem.Utils.Extensions;

namespace InteliSystem.InteliMarketPlace.Applications.LoginsApp
{
    public class Login
    {
        private string password;
        public string EMail { get; set; }
        public string PassWord { get => password.ToSha512($"{password}{this.EMail.ToLower()}"); set => password = value; }
        public string Device { get; set; }
    }
}