using System;
using Microsoft.VisualBasic;

namespace InteliSystem.Utils.Globals.Functions
{
    public static class GenereteIdValue
    {
        public static string Generete(string initials)
        {
            var id = $"{DateTime.Now.ToString("yyyyMMddhhmmssms")}{initials}{RandonNumber.GenereteToString(10000, 99999)}";
            return id;
        }
    }
}