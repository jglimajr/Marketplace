using System;
using System.Linq;

namespace InteliSystem.Utils.Extensions
{
    public static class ExtensionsEnum
    {
        public static string Description(this Enum value)
        {
            var etype = value.GetType();
            var memberinfo = etype.GetMember(value.ToString());

            if (memberinfo.Length <= 0)
                return value.ToString();

            var attribs = memberinfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            return attribs.Any() ? ((System.ComponentModel.DescriptionAttribute)attribs.ElementAt(0)).Description : value.ToString();
        }
    }
}