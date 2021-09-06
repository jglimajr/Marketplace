using InteliSystem.Utils.Globals.Classes;

namespace InteliSystem.InteliMarketPlace.Domains.Products
{
    public class Manufacturer : ClassBaseGuid
    {
        public int Name { get; private set; }
        public string GovernmentId { get; private set; }
        public string SocialReason { get; private set; }

    }
}