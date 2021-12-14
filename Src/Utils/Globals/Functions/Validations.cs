using InteliSystem.Utils.Extensions;

namespace InteliSystem.Utils.Globals.Functions;
public static class Validations
{
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
    public static bool IsCnpj(string value)
    {
        var mult1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var mult2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        var cnpj = value.NumbersOnly();
        if (cnpj.Length != 14)
        {
            return false;
        }

        var cnpjAux = cnpj.Substring(0, 12);
        var soma = 0;
        for (int i = 0; i < 12; i++)
        {
            soma += int.Parse(cnpjAux[i].ToString()) * mult1[i];
        }

        var resto = 0;
        if ((soma % 11) >= 2)
        {
            resto = 11 - (soma % 11);
        }
        var digito = resto.ToString();
        cnpjAux += digito;
        soma = 0;

        for (int i = 0; i < 13; i++)
        {
            soma += int.Parse(cnpjAux[i].ToString()) * mult2[i];
        }
        resto = 0;
        if ((soma % 11) >= 2)
        {
            resto = 11 - (soma % 11);
        }
        digito += resto.ToString();

        return cnpj.EndsWith(digito);
    }

    public static bool IsNis(string values)
    {
        var mult = new int[] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var nis = values.NumbersOnly().PadLeft(11, '0');
        if (nis.Length != 11)
        {
            return false;
        }

        var soma = 0;
        for (int i = 0; i < mult.Length; i++)
        {
            soma += int.Parse(nis[i].ToString()) * mult[i];
        }

        var resto = 0;
        if ((soma % 11) >= 2)
        {
            resto = 11 - (soma % 11);
        }
        return nis.EndsWith(resto.ToString());
    }
}