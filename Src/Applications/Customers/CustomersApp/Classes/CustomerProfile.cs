using InteliSystem.Utils.Globals.Classes;
using InteliSystem.Utils.Globals.Enumerators;

namespace InteliSystem.InteliMarketPlace.Applications.CustomersApp.Classes
{
    public class CustomerProfile : ClassBaseAppGuid
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public GenderValues Gender { get; set; }
        public string EMail { get; set; }
    }
}