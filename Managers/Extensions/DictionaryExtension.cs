using System.Collections.Generic;
using System.Linq;

namespace SirenixPowered.ExtensionMethods
{
    public static class DictionaryExtension
    {
        /// <summary>
        /// Método que adiciona a chave e o valor fornecidos ao dicionário fornecido apenas se a chave NÃO estiver presente no dicionário.
        /// Isto será útil para evitar padrões repetitivos de "if (! Containskey ()) e adicionar" de codificação.
        /// </summary>
        /// <param name = "dict"> O dicionário fornecido. </param>
        /// <param name = "key"> A chave fornecida. </param>
        /// <param name = "value"> O valor fornecido. </param>
        /// <returns> Verdadeiro se adicionado com sucesso, falso caso contrário. </returns>
        /// <typeparam name = "TKey"> Refere-se ao tipo TKey. </typeparam>
        /// <typeparam name = "TValue"> Refere-se ao tipo TValue. </typeparam>
        public static bool AddIfNotExists<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                return false;
            }

            dict.Add(key, value);

            return true;
        }

        /// <resumo>
        /// Método que adiciona a chave e o valor fornecidos ao dicionário fornecido se a chave NÃO estiver presente no dicionário.
        /// Se presente, o valor será substituído pelo novo valor.
        /// </summary>
        /// <param name = "dict"> O dicionário fornecido. </param>
        /// <param name = "key"> A chave fornecida. </param>
        /// <param name = "value"> O valor fornecido. </param>
        /// <typeparam name = "TKey"> Refere-se ao tipo de chave. </typeparam>
        /// <typeparam name = "TValue"> Refere-se ao tipo de valor. </typeparam>
        public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }          
        }

        /// <resumo>
        /// Método que adiciona a lista de objetos KeyValuePair fornecidos ao dicionário fornecido. Se uma chave já estiver presente no dicionário,
        /// então um erro será gerado.
        /// </summary>
        /// <param name = "dict"> O dicionário fornecido. </param>
        /// <param name = "kvpList"> A lista de objetos KeyValuePair. </param>
        /// <typeparam name = "TKey"> Refere-se ao tipo TKey. </typeparam>
        /// <typeparam name = "TValue"> Refere-se ao tipo TValue. </typeparam>
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, List<KeyValuePair<TKey, TValue>> kvpList)
        {
            foreach (KeyValuePair<TKey, TValue> kvp in kvpList)
            {
                dict.Add(kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Converte uma enumeração de agrupamentos em um Dicionário desses agrupamentos.
        /// </summary>
        /// <typeparam name = "TKey"> Tipo de chave do agrupamento e dicionário. </typeparam>
        /// <typeparam name = "TValue"> Tipo de elemento da lista de agrupamento e dicionário. </typeparam>
        /// <param name = "groupings"> A enumeração de agrupamentos de uma cláusula GroupBy (). </param>
        /// <returns> Um dicionário de agrupamentos de forma que a chave do dicionário seja do tipo TKey e o valor seja Lista do tipo TValue. </returns>
        /// <example> results = productList.GroupBy (product => product.Category) .ToDictionary (); </example>
        /// <remarks>http://extensionmethod.net/csharp/igrouping/todictionary-for-enumerations-of-groupings</remarks>
        public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groupings)
        {
            return groupings.ToDictionary(group => group.Key, group => group.ToList());
        }

        public static KeyValuePair<TKey, TValue> GetKeyValuePair<TKey, TValue> (this IDictionary<TKey, TValue> dictionary,TKey key) => new KeyValuePair<TKey, TValue>(key, dictionary[key]);
    }
}