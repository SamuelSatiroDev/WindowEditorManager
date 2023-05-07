using System;
using System.Linq;

namespace SirenixPowered.ExtensionMethods
{
    public enum PathDirectory { Custom, Data, Streaming, Persistent }
    public enum ToggleEnum { Enable, Disable }

    public static class EnumExtension
    {
        /// <summary>
        /// Converte uma string em um enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="ignoreCase">verdadeiro para ignorar a string</param>
        public static T ToEnum<T>(this string s, bool ignoreCase) where T : struct
        {
            // sair se nulo
            if (s.IsNullOrEmpty())
            {
                return default(T);
            }
                

            Type genericType = typeof(T);

            if (!genericType.IsEnum)
            {
                return default(T);
            }

            try
            {
                return (T)Enum.Parse(genericType, s, ignoreCase);
            }

            catch (Exception)
            {
                // não conseguiu analisar, então tente uma maneira diferente de obter os enums
                Array ary = Enum.GetValues(genericType);

                foreach (T en in ary.Cast<T>()
                    .Where(en =>
                        (string.Compare(en.ToString(), s, ignoreCase) == 0) ||
                        (string.Compare((en as Enum).ToString(), s, ignoreCase) == 0)))
                {
                    return en;
                }

                return default(T);
            }
        }

        /// <summary>
        /// Converte uma string em um enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        public static T ToEnum<T>(this string s) where T : struct
        {
            return s.ToEnum<T>(false);
        }

        public static int Count(this Enum EnumType)
        {
            return System.Enum.GetValues(EnumType.GetType()).Length;
        }
    }
}