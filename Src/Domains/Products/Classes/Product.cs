using System;
using InteliSystem.Utils.Globals.Classes;

namespace InteliSystem.InteliMarketPlace.Domains.Products
{
    public class Product : ClassBaseGuid
    {
        public string EanCode { get; private set; }
        public Guid ManufacturerId { get; private set; }
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string PurchasingUnit { get; private set; }
        public decimal ConversionRate { get; private set; }
        public string SalesUnit { get; private set; }
        public string ImageId { get; private set; }
    }
}