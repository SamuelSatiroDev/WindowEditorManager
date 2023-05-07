namespace SirenixPowered.ExtensionMethods
{
    public static class ObjectExtension
    {
        /// <resumo>
        /// Verifica se o objeto fornecido é de {T}.
        /// </summary>
        /// <param name = "obj"> O objeto a ser verificado. </param>
        /// <typeparam name = "T"> Refere-se ao tipo de dados de destino. </typeparam>
        /// <returns> Verdadeiro se o objeto fornecido for do tipo T, falso caso contrário. </returns>
        public static bool IsA<T>(this object obj) => obj is T;

        /// <resumo>
        /// Verifica se o objeto fornecido NÃO é do tipo T.
        /// </summary>
        /// <param name = "obj"> O objeto a ser verificado. </param>
        /// <typeparam name = "T"> Refere-se ao tipo de dados de destino. </typeparam>
        /// <returns> Verdadeiro se o objeto fornecido NÃO for do tipo T, falso caso contrário. </returns>
        public static bool IsNotA<T>(this object obj) => obj.IsA<T>().Toggle();

        /// <resumo>
        /// Tenta lançar o objeto fornecido para o tipo T
        /// </summary>
        /// <param name = "obj"> O objeto a ser lançado. </param>
        /// <typeparam name = "T"> Refere-se ao tipo de dados de destino. </typeparam>
        /// <returns> Retorna os objetos fundidos. Nulo se a conversão falhar. </returns>
        public static T As<T>(this object obj) where T : class => obj as T;

        /// <resumo>
        /// Verifica se o objeto fornecido é Nulo.
        /// </summary>
        /// <param name = "obj"> O objeto a ser verificado. </param>
        /// <returns> Verdadeiro se o objeto for Nulo, falso caso contrário. </returns>
        public static bool IsNull(this object obj) => obj == null;

        /// <resumo>
        /// Verifica se o objeto fornecido NÃO é nulo.
        /// </summary>
        /// <param name = "obj"> O objeto a ser verificado. </param>
        /// <returns> Verdadeiro se o objeto NÃO for nulo, caso contrário, falso. </returns>
        public static bool IsNotNull(this object obj) => obj != null ? true : false;

        /// <summary>
        /// Verifica se o objeto fornecido tem algum valor. Semelhante a IsNotNull ().
        /// </summary>
        /// <param name = "obj"> O objeto a ser verificado. </param>
        /// <returns> Verdadeiro se o objeto NÃO for nulo, caso contrário, falso. </returns>
        public static bool HasValue(this object obj) => !(obj == null);
    }
}