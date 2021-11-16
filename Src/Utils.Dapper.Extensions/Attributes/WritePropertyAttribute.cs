using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InteliSystem.Utils.Dapper.Extensions.Attributes
{
    public class WritePropertyAttribute : Attribute
    {
        private bool _write;
        public WritePropertyAttribute(bool write = true)
        {
            this._write = write;
        }

        public bool WriteProperty => this._write;
    }
}