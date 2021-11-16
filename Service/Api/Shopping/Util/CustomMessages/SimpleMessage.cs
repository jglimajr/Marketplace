using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Util.CustomMessages
{
    public class SimpleMessage
    {
        private string _key;
        private string _message;
        public SimpleMessage(string key, string message)
        {
            this._key = key;
            this._message = message;
        }

        public string Key => this._key;
        public string Message => this._message;
    }
}