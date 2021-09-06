using System;
using System.Collections.Generic;
using InteliSystem.Utils.Extensions;
using InteliSystem.Utils.Globals.Enumerators;
using InteliSystem.Utils.Globals.Functions;
using Utils.Globals.Notifications;

namespace InteliSystem.InteliMarketPlace.Domains.Products.Classes
{
    public class Category : InteliNotification
    {
        private Category()
        {
            this.Id = GenereteIdValue.Generete("CAT");
            this.Status = StatusValues.Active;
            this.DateTimeCreation = DateTime.Now;
            this.DateTimeUpdate = DateTime.Now;
        }

        public Category(string name, string description)
            : this()
        {
            this.Name = name;
            this.Description = description;
            Validate();
        }

        public Category(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            Validate();
        }

        private void Validate()
        {
            if (this.Id.IsEmpty())
                this.AddNotification("Category.Id", "Id not informed");
                
            if (this.Name.IsEmpty())
                this.AddNotification("Category.Name", "Name not informed");
        }

        public override bool Equals(object obj)
        {
            return obj is Category category &&
                   Id == category.Id &&
                   Name == category.Name;
        }

        public override int GetHashCode()
        {
            int hashCode = 1829809407;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            return hashCode;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public StatusValues Status { get; private set; }
        public DateTime DateTimeCreation { get; private set; }
        public DateTime? DateTimeUpdate { get; private set; }
    }
}