using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InteliSystem.InteliMarketPlace.Applications.LoginsApp
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Device { get; set; }
        public string EMail { get; set; }
        public DateTime DateTimeCreate { get; set; }
    }
}