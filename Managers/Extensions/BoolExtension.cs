namespace SirenixPowered.ExtensionMethods
{
    public static class BoolExtension
    {
        /// <summary>
        /// Checks if the given boolean item is true.
        /// </summary>
        /// <param name="item">Item a ser verificado.</param>
        /// <returns>Verdadeiro se o valor for verdadeiro, falso caso contrário.</returns>
        public static bool IsTrue(this bool item) => item;

        /// <summary>
        /// Verifica se o item booleano fornecido é falso.
        /// </summary>
        /// <param name="item">Item a ser verificado.</param>
        /// <returns>Verdadeiro se o valor for falso, falso caso contrário.</returns>
        public static bool IsFalse(this bool item) => !item;

        /// <summary>
        /// Verifica se o item booleano fornecido NÃO é verdadeiro.
        /// </summary>
        /// <param name="item">Item a ser verificado.</param>
        /// <returns>Verdadeiro se o item for falso, falso caso contrário.</returns>
        public static bool IsNotTrue(this bool item) => !item.IsTrue();

        /// <summary>
        /// Verifica se o item booleano fornecido NÃO é falso.
        /// </summary>
        /// <param name="item">Item a ser verificado.</param>
        /// <returns>Verdadeiro se o valor for verdadeiro, falso caso contrário.</returns>
        public static bool IsNotFalse(this bool item) => !item.IsFalse();

        /// <summary>
        /// Alterna o item booleano fornecido e retorna o valor alternado.
        /// </summary>
        /// <param name="item">Item a ser alternado.</param>
        /// <returns>O valor alternado.</returns>
        public static bool Toggle(this bool item) => !item;

        /// <summary>
        /// Converte o valor booleano fornecido em inteiro.
        /// </summary>
        /// <param name="item">A variável booleana.</param>
        /// <returns>Retorna 1 se verdadeiro, 0 caso contrário.</returns>
        public static int ToInt(this bool item) => item ? 1 : 0;

        /// <summary>
        /// Converte o valor inteiro fornecido em booleano.
        /// </summary>
        /// <param name="item">A variável inteiro.</param>
        /// <returns>Retorna verdadeiro se >= 1, Falso se 0.</returns>
        public static bool ToBool(this int item) => item == 0 ? false : true;

        /// <summary>
        /// Retorna a representação de string inferior de booleano.
        /// </summary>
        /// <param name="item">A variável booleana.</param>
        /// <returns>Retorna "True" ou "False".</returns>
        public static string ToLowerString(this bool item) => item.ToString().ToLower();

        /// <summary>
        /// Retorna "Yes" ou "No" com base no valor booleano fornecido.
        /// </summary>
        /// <param name="item">O valor booleano.</param>
        /// <returns>Sim se o valor fornecido for verdadeiro, caso contrário Não.</returns>
        public static string ToYesNo(this bool item) => item.ToString("Yes", "No");

        /// <summary>
        /// Retorna o trueString ou falseString com base no valor booleano fornecido.
        /// </summary>
        /// <param name="item">O valor booleano.</param>
        /// <param name="trueString">Valor a ser retornado se a condição for verdadeira.</param>
        /// <param name="falseString">Valor a ser devolvido se a condição for falsa.</param>
        /// <returns>Retorna trueString se o valor fornecido for verdadeiro, caso contrário, falseString.</returns>
        public static string ToString(this bool item, string trueString, string falseString) => item.ToType<string>(trueString, falseString);

        /// <summary>
        /// Retorna o trueValue ou o falseValue com base no valor booleano fornecido.
        /// </summary>
        /// <param name="item">O valor booleano.</param>
        /// <param name="trueValue">Valor a ser retornado se a condição for verdadeira.</param>
        /// <param name="falseValue">Valor a ser devolvido se a condição for falsa.</param>
        /// <typeparam name="T">Instância de qualquer classe.</typeparam>
        /// <returns>Retorna trueValue se o valor fornecido for verdadeiro, caso contrário, falseValue.</returns>
        public static T ToType<T>(this bool item, T trueValue, T falseValue) => item ? trueValue : falseValue;

        /// <summary>
        /// Retorna o trueValue ou o falseValue com base no valor booleano fornecido.
        /// </summary>
        /// <param name="item">O valor booleano.</param>
        /// <param name="trueValue">Valor a ser retornado se a condição for verdadeira.</param>
        /// <param name="falseValue">Valor a ser devolvido se a condição for falsa.</param>
        /// <typeparam name="T">Instância de qualquer classe.</typeparam>
        /// <returns>Retorna trueValue se o valor fornecido for verdadeiro, caso contrário, falseValue.</returns>
        public static void ToInvoke(this bool item, System.Action OnTrue, System.Action OnFalse)
        {
            if (item == true)
            {
                OnTrue?.Invoke();
            }
            else
            {
                OnFalse?.Invoke();
            }
        }
    }
}