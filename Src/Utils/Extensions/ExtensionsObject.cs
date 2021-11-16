using System;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;

namespace InteliSystem.Utils.Extensions
{
    public static class ExtensionsObject
    {
        public static bool IsNull(this object value)
        {
            return (value == null);
        }

        public static bool IsNotNull(this object value)
        {
            return (value != null);
        }

        public static string ObjectToString(this object value)
        {
            if (value.IsNull())
                return string.Empty;

            return value.ToString();
        }


        public static string ToJson<T>(this T value) where T : class => JsonSerializer.Serialize<T>(value);

        public static T Load<T>(this T innerobject, object loadedobject) where T : class
        {
            if (loadedobject.IsNull())
                return innerobject;

            var innertype = innerobject.GetType();
            var loadedtype = loadedobject.GetType();

            var innerprops = innertype.GetProperties(System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var loadedprops = loadedtype.GetProperties(System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Parallel.ForEach(innerprops, innerprop =>
             {
                 Parallel.ForEach(loadedprops, loadedprop =>
                 {
                     var custattibs = innerprop.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute));
                     if (custattibs.IsNull())
                     {
                         if (innerprop.Name == loadedprop.Name)
                         {
                             var value = loadedprop.GetValue(loadedobject);
                             var innervalue = innerprop.GetValue(innerobject);
                             if (innerprop.PropertyType.ObjectToString().ToLower().Contains("intelisystem") && innerprop.PropertyType.BaseType == typeof(System.Object))
                             {
                                 innervalue.Load(value);
                                 innerprop.SetValue(innerobject, innervalue);
                             }
                             else
                             if (value.IsNotNull() && innervalue.ObjectToString() != value.ObjectToString())
                             {
                                 if ((loadedprop.PropertyType == typeof(DateTime) || loadedprop.PropertyType == typeof(DateTime?)) && innerprop.PropertyType == typeof(string))
                                 {
                                     var valueAux = (DateTime)value;
                                     innerprop.SetValue(innerobject, valueAux.ToStringDateBrazilian());
                                 }
                                 else if ((innerprop.PropertyType == typeof(DateTime) || innerprop.PropertyType == typeof(DateTime?)) && loadedprop.PropertyType == typeof(string))
                                 {
                                     var valueAux = value.ObjectToString().ToDateTime();
                                     innerprop.SetValue(innerobject, valueAux);
                                 }
                                 else
                                     innerprop.SetValue(innerobject, value);
                             }
                         }
                         if (innerprop.Name == loadedprop.Name)
                         {
                             var value = loadedprop.GetValue(loadedobject);
                             var innervalue = innerprop.GetValue(innerobject);
                             if (innerprop.PropertyType.ObjectToString().ToLower().Contains("intelisystem") && innerprop.PropertyType.BaseType == typeof(System.Object))
                             {
                                 innervalue.Load(value);
                                 innerprop.SetValue(innerobject, innervalue);
                             }
                             else
                             if (value.IsNotNull() && innervalue.ObjectToString() != value.ObjectToString())
                             {
                                 if ((loadedprop.PropertyType == typeof(DateTime) || loadedprop.PropertyType == typeof(DateTime?)) && innerprop.PropertyType == typeof(string))
                                 {
                                     var valueAux = (DateTime)value;
                                     innerprop.SetValue(innerobject, valueAux.ToStringDateBrazilian());
                                 }
                                 else if ((innerprop.PropertyType == typeof(DateTime) || innerprop.PropertyType == typeof(DateTime?)) && loadedprop.PropertyType == typeof(string))
                                 {
                                     var valueAux = value.ObjectToString().ToDateTime();
                                     innerprop.SetValue(innerobject, valueAux);
                                 }
                                 else
                                     innerprop.SetValue(innerobject, value);
                             }
                         }
                     }
                 });
             });

            return innerobject;
        }
    }
}