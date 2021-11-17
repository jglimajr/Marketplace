using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InteliSystem.InteliMarketPlace.Applications.LoginsApp.Classes
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Device { get; set; }
        public DateTime DateTimeCreate { get; set; }
    }
}