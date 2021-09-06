using System;
using System.Globalization;

namespace InteliSystem.Utils.Extensions
{
    public static class ExtensionsNumber
    {
        public static short Sum(params short[] values)
        {
            short retorno = 0;
            for (int i = 0; i < values.Length; i++)
            {
                retorno += values[i];
            }
            return retorno;
        }
        public static int Sum(params int[] values)
        {
            int retorno = 0;
            for (int i = 0; i < values.Length; i++)
            {
                retorno += values[i];
            }
            return retorno;
        }
        public static long Sum(params long[] values)
        {
            long retorno = 0;
            for (int i = 0; i < values.Length; i++)
            {
                retorno += values[i];
            }
            return retorno;
        }
        public static double Sum(params double[] values)
        {
            double retorno = 0;
            for (int i = 0; i < values.Length; i++)
            {
                retorno += values[i];
            }
            return retorno;
        }
        public static decimal Sum(params decimal[] values)
        {
            decimal retorno = 0;
            for (int i = 0; i < values.Length; i++)
            {
                retorno += values[i];
            }
            return retorno;
        }
        public static double Trunc(this double value, int decimalplaces)
        {
            var valueAux = value.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
            var parts = valueAux.Split(",");
            if (parts.Length == 1)
                return parts[0].ToDouble();
            var decimals = "";
            var virgula = "";
            var aux = parts[1];
            for (int i = 0; i < aux.Length; i++)
            {
                if (i == decimalplaces)
                    break;
                decimals += aux.Substring(i, 1);
                virgula = ",";
            }
            valueAux = string.Concat(parts[0], virgula, decimals);
            return valueAux.ToDouble();
        }
        public static double Round(this double value, int decimalplaces)
        {
            value = Math.Round(value, decimalplaces);
            return value;
        }
        public static decimal Trunc(this decimal value, int decimalplaces)
        {

            var valueAux = value.ToString(CultureInfo.CreateSpecificCulture("pt-BR"));
            var parts = valueAux.Split(",");
            if (parts.Length == 1)
                return parts[0].ToDecimal();
            var decimals = "";
            var virgula = "";
            var aux = parts[1];
            for (int i = 0; i < aux.Length; i++)
            {
                if (i == decimalplaces)
                    break;
                decimals += aux.Substring(i, 1);
                virgula = ",";
            }
            valueAux = string.Concat(parts[0], virgula, decimals);
            return valueAux.ToDecimal();

        }
        public static decimal Round(this decimal value, int decimalplaces)
        {
            value = Math.Round(value, decimalplaces);
            return value;
        }

        public static string FormatDecimal(this decimal value, int decimalplaces)
        {
            var retorno = string.Format("{0:0." + new string('0', decimalplaces) + "}", value);
            return retorno;
        }

        public static string ZeroLeft(this object value, int size)
        {
            var aux = value.ObjectToString().NumbersOnly().PadLeft(size, '0');
            return aux;
        }

        public static string ZeroLeft(this int value, int size)
        {
            var aux = value.ObjectToString().NumbersOnly().PadLeft(size, '0');
            return aux;
        }
        public static string ZeroLeft(this double value, int size)
        {
            var aux = value.ObjectToString().NumbersOnly().PadLeft(size, '0');
            return aux;
        }

        public static string ZeroLeft(this decimal value, int size)
        {
            var aux = value.ObjectToString().NumbersOnly().PadLeft(size, '0');
            return aux;
        }

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.NumbersOnly();

            if (cpf.IsEmpty())
            {
                return false;
            }

            cpf = cpf.ZeroLeft(11);

            if (cpf.Equals(new string('0', 11)) || cpf.Equals(new string('1', 11)) || cpf.Equals(new string('2', 11)) || cpf.Equals(new string('3', 11)) || cpf.Equals(new string('4', 11)) ||
                cpf.Equals(new string('5', 11)) || cpf.Equals(new string('6', 11)) || cpf.Equals(new string('7', 11)) || cpf.Equals(new string('8', 11)) || cpf.Equals(new string('9', 11)))
            {
                return false;
            }

            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool IsNumeric(object value)
        {
            return double.TryParse(value.ObjectToString(), out double d);
        }
    }
}