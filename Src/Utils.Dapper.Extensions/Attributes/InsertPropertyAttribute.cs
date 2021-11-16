using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InteliSystem.Utils.Dapper.Extensions.Attributes
{
    public class InsertPropertyAttribute : Attribute
    {
        private bool _insert;
        public InsertPropertyAttribute(bool insert = true)
        {
            this._insert = insert;
        }

        public bool InsertProperty => this._insert;
    }
}