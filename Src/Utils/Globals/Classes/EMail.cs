using System;
using InteliSystem.Utils.Extensions;
using Utils.Globals.Notifications;

namespace Utils.Globals.Classes
{
    public class EMail : InteliNotification
    {
        public EMail(string value = null)
            : base()
        {
            if (value.IsNotEmpty())
            {
                if (value.IsEMail())
                    Value = value;
                else
                    AddNotification("E-Mail", "E-Mail is not valid");
            }
        }
        public string Value { get; private set; }

        public override string ToString()
            => this.Value;
    }
}
