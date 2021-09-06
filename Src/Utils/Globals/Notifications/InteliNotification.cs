using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace Utils.Globals.Notifications
{
    public abstract class InteliNotification
    {
        private Dictionary<string, string> _messages;
        public InteliNotification()
        {
            _messages = new Dictionary<string, string>();
        }

        public void AddNotification(string property, string message)
        {
            this._messages.Add(property, message);
        }
        [Write(false)]
        public bool ExistNotifications => (this._messages.Count > 0);

    }
}
