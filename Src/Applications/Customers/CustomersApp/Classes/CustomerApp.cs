using System;
using System.Text.Json.Serialization;
using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;

namespace InteliSystem.InteliMarketPlace.Applications.CustomersApp
{
    public class CustomerApp
    {
        public string Name { get; set; }
        public string EMail { get; set; }
        public string PassWord { get; set; }

    }
}