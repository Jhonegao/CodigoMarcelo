using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Extensions
{
    static class StringExtensions
    {
        public static string IsValidCNH(this string cnh)
        {
            bool isValid = false;
            var firstChar = cnh[0];
            if (cnh.Length == 11 && cnh != new string('1', 11))
            {
                var dsc = 0;
                var v = 0;
                for (int i = 0, j = 9; i < 9; i++, j--)
                {
                    v += (Convert.ToInt32(cnh[i].ToString()) * j);
                }

                var vl1 = v % 11;
                if (vl1 >= 10)
                {
                    vl1 = 0;
                    dsc = 2;
                }
                v = 0;
                for (int i = 0, j = 1; i < 9; ++i, ++j)
                {
                    v += (Convert.ToInt32(cnh[i].ToString()) * j);
                }
                var x = v % 11;
                var vl2 = (x >= 10) ? 0 : x - dsc;

                isValid = vl1.ToString() + vl2.ToString() == cnh.Substring(cnh.Length - 2, 2);

                if (isValid)
                {
                    return "";
                }
            }
            return "CNH inválida.";
        }

        public static string IsValidCPF(this string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return "CPF deve conter 11 caracteres.";
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
            bool val = cpf.EndsWith(digito);
            if (val)
            {
                return "";
            }
            return "CPF inválido.";
        }
    }
}
