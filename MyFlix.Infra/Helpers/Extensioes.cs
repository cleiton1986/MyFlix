using MyFlix.Infra.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyFlix.Infra.Helpers
{
    public static class Extensioes
    {
        public static string EnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public static int EnumToInt(this Enum value)
        {
            int intValue = (int)Convert.ChangeType(value, value.GetTypeCode());
            return intValue;
        }
        public static bool ValidateString(this string value, string mensagem)
        {
            var validate = false;

            if (string.IsNullOrWhiteSpace(value) || value.Equals("string"))
            {
                Notificacao.NotifyList(mensagem);
                validate = true;
            }


            return validate;
        }
        public static bool ValidateCep(this string cep, string mensagem)
        {
            var validate = false;
            if (!string.IsNullOrWhiteSpace(cep))
            {
                string pattern = @"^\d{5}-\d{3}$";
                validate = Regex.IsMatch(cep, pattern);

                if (!validate)
                {
                    Notificacao.NotifyList(mensagem);
                }
            }
            return validate;
        }
        public static bool ValidateBooleam(this bool value, string mensagem)
        {
            if (!value)
            {
                Notificacao.NotifyList(mensagem);
            }
            return value;
        }
        public static bool ValidateDecimal(this decimal value, string mensagem)
        {
            var validate = value <= 0;
            if (validate)
            {
                Notificacao.NotifyList(mensagem);
            }
            return validate;
        }

        public static bool ValidateInt(this int value, string mensagem)
        {
            var validate = value <= 0;
            if (validate)
            {
                Notificacao.NotifyList(mensagem);
            }
            return validate;
        }
        public static int GetNumero()
        {
            Random a = new Random();

            int c = a.Next(100, 999);
            return c;
        }
        public static bool ValidateEmail(this string value, string mensagem)
        {
            var validate = false;
            if (!string.IsNullOrWhiteSpace(value))
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex regex = new Regex(pattern);

                validate = regex.IsMatch(value);

                if (!validate)
                {
                    Notificacao.NotifyList(mensagem);
                }
            }
            return validate;
        }

        public static bool ValidateDate(this DateTime? value, string mensagem)
        {
            var validate = false;

            if (!value.HasValue)
                return validate;

            string dataString = value.Value.ToString("dd/MM/yyyy");
            string formato = "dd/MM/yyyy";
            DateTime dataValida;

            if (DateTime.TryParseExact(dataString, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out dataValida))
            {
                validate = true;
            }

            if (dataValida == DateTime.MinValue)
                Notificacao.NotifyList(mensagem);


            return validate;
        }

        public static string ConvertDateToString(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy");
        }
        public static DateTime ConvertStringToDate(this string value)
        {
            DateTime data;

            if (DateTime.TryParse(value, out data))
            {
                return data;
            }

            return data;
        }
    }
}
