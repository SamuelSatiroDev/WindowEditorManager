using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SirenixPowered.ExtensionMethods
{
    public static class ListExtension
    {
        private static string propertyName = string.Empty;
        private static bool setReverse = false;

        private enum VariablesType
        {
            String,
            Char,
            Byte,
            Sbyte,
            Short,
            Unshort,
            Int,
            Uint,
            Long,
            Ulong,
            Float,
            Double,
            Decimal,
            Bool,
            GameObject,
        }

        private static Dictionary<Type, VariablesType> typeDict = new Dictionary<Type, VariablesType>
        {
        {typeof(string), VariablesType.String},
        {typeof(char), VariablesType.Char},
        {typeof(byte), VariablesType.Byte},
        {typeof(sbyte), VariablesType.Sbyte},
        {typeof(short), VariablesType.Short},
        {typeof(ushort), VariablesType.Unshort},
        {typeof(int), VariablesType.Int},
        {typeof(uint), VariablesType.Uint},
        {typeof(long), VariablesType.Long},
        {typeof(ulong), VariablesType.Ulong},
        {typeof(float), VariablesType.Float},
        {typeof(double), VariablesType.Double},
        {typeof(decimal), VariablesType.Decimal},
        {typeof(bool), VariablesType.Bool},
        {typeof(GameObject), VariablesType.GameObject},
        };

        #region AutoComplete
        public static void AutoComplete(this List<string> Result, List<string> WordsDataBase, string InputText, string OldText, int ResultLimit)
        {
            string _oldString = OldText;
            OldText = InputText;

            if (OldText == string.Empty)
            {
                Result.Clear();
            }

            if (!string.IsNullOrEmpty(OldText) && OldText.Length > _oldString.Length)
            {
                List<string> _found = WordsDataBase.FindAll(w => w.StartsWith(OldText));
                if (_found.Count > 0)
                {
                    Result.Clear();

                    for (int i = 0; i < _found.Count; i++)
                    {
                        if (i < ResultLimit)
                        {
                            Result.Add(_found[i]);
                        }
                    }
                }
            }
        }

        public static void AutoComplete(this List<string> Result, List<string> WordsDataBase, string InputText, string OldText)
        {
            string _oldString = OldText;
            OldText = InputText;

            if (OldText == string.Empty)
            {
                Result.Clear();
            }

            if (!string.IsNullOrEmpty(OldText) && OldText.Length > _oldString.Length)
            {
                List<string> _found = WordsDataBase.FindAll(w => w.StartsWith(OldText));
                if (_found.Count > 0)
                {
                    Result.Clear();

                    for (int i = 0; i < _found.Count; i++)
                    {
                        Result.Add(_found[i]);
                    }
                }
            }
        }

        /*
        [SerializeField] private string _inputText = string.Empty;
        [SerializeField] private byte _resultLimit = 2;
        [SerializeField] private List<string> _result = new List<string>();

        private List<string> _wordsDataBase = new List<string>();
        private string _oldString = string.Empty;

        private void Start()
        {
            _wordsDataBase.Add("Word One");
            _wordsDataBase.Add("Word Two");
            _wordsDataBase.Add("Word Three");
        }

        private void Update()
        {
            _result.AutoComplete(_wordsDataBase, _inputText, _oldString, _resultLimit);
        }
        */
        #endregion

        #region SortList
        /// <summary>
        /// Classificar lista através do nome da variável.
        /// </summary>
        /// <returns></returns>
        public static List<T> SortList<T>(this List<T> list, string variableName, bool reverse = false)
        {
            setReverse = reverse;
            propertyName = variableName;
            list.Sort(SortByList);
            return list;
        }

        /// <summary>
        /// Classificar lista.
        /// </summary>
        /// <returns></returns>
        public static List<T> SortList<T>(this List<T> list, bool reverse = false)
        {
            setReverse = reverse;
            list.Sort(SortByList);
            return list;
        }

        private static int SortByList<T>(T item1, T item2)
        {
            var _item1 = ItemVariableValue(item1);
            var _item2 = ItemVariableValue(item2);

            switch (typeDict[_item1.GetType()])
            {
                case VariablesType.String:
                    string string1 = setReverse ? (string)_item2 : (string)_item1;
                    string string2 = setReverse ? (string)_item1 : (string)_item2;

                    return string1.CompareTo(string2);

                case VariablesType.Char:
                    char char1 = setReverse ? (char)_item2 : (char)_item1;
                    char char2 = setReverse ? (char)_item1 : (char)_item2;

                    return char1.CompareTo(char2);

                case VariablesType.Byte:
                    byte byte1 = setReverse ? (byte)_item2 : (byte)_item1;
                    byte byte2 = setReverse ? (byte)_item1 : (byte)_item2;

                    return byte1.CompareTo(byte2);

                case VariablesType.Sbyte:
                    sbyte sbyte1 = setReverse ? (sbyte)_item2 : (sbyte)_item1;
                    sbyte sbyte2 = setReverse ? (sbyte)_item1 : (sbyte)_item2;

                    return sbyte1.CompareTo(sbyte2);

                case VariablesType.Short:
                    short short1 = setReverse ? (short)_item2 : (short)_item1;
                    short short2 = setReverse ? (short)_item1 : (short)_item2;

                    return short1.CompareTo(short2);

                case VariablesType.Unshort:
                    ushort ushort1 = setReverse ? (ushort)_item2 : (ushort)_item1;
                    ushort ushort2 = setReverse ? (ushort)_item1 : (ushort)_item2;

                    return ushort1.CompareTo(ushort2);

                case VariablesType.Int:
                    int int1 = setReverse ? (int)_item2 : (int)_item1;
                    int int2 = setReverse ? (int)_item1 : (int)_item2;

                    return int1.CompareTo(int2);

                case VariablesType.Uint:
                    uint uint1 = setReverse ? (uint)_item2 : (uint)_item1;
                    uint uint2 = setReverse ? (uint)_item1 : (uint)_item2;

                    return uint1.CompareTo(uint2);

                case VariablesType.Long:
                    long long1 = setReverse ? (long)_item2 : (long)_item1;
                    long long2 = setReverse ? (long)_item1 : (long)_item2;

                    return long1.CompareTo(long2);

                case VariablesType.Ulong:
                    ulong ulong1 = setReverse ? (ulong)_item2 : (ulong)_item1;
                    ulong ulong2 = setReverse ? (ulong)_item1 : (ulong)_item2;

                    return ulong1.CompareTo(ulong2);

                case VariablesType.Float:
                    float float1 = setReverse ? (float)_item2 : (float)_item1;
                    float float2 = setReverse ? (float)_item1 : (float)_item2;

                    return float1.CompareTo(float2);

                case VariablesType.Double:
                    double double1 = setReverse ? (double)_item2 : (double)_item1;
                    double double2 = setReverse ? (double)_item1 : (double)_item2;

                    return double1.CompareTo(double2);

                case VariablesType.Decimal:
                    decimal decimal1 = setReverse ? (decimal)_item2 : (decimal)_item1;
                    decimal decimal2 = setReverse ? (decimal)_item1 : (decimal)_item2;

                    return decimal1.CompareTo(decimal2);

                case VariablesType.Bool:
                    bool bool1 = setReverse ? (bool)_item2 : (bool)_item1;
                    bool bool2 = setReverse ? (bool)_item1 : (bool)_item2;

                    return bool1.CompareTo(bool2);

                case VariablesType.GameObject:
                    GameObject _obj1 = (GameObject)_item1;
                    GameObject _obj2 = (GameObject)_item2;

                    string objName1 = setReverse ? _obj2.name : _obj1.name;
                    string objName2 = setReverse ? _obj1.name : _obj2.name;

                    return objName1.CompareTo(objName2);

                default:
                    return default;
            }
        }

        private static object ItemVariableValue<T>(T item)
        {
            object output = item.GetType().GetField(propertyName) == null ? item : item.GetType().GetField(propertyName).GetValue(item);
            return output;
        }
        #endregion

        #region Contains
        public static bool Contains<T>(this List<T> list, object value, string propertyName)
        {
            List<object> property = new List<object>();

            for (int i = 0; i < list.Count; i++)
            {
                object obj = ItemVariableValue(list[i], propertyName);
                property.Add(obj);
            }

            return property.Contains(value);
        }

        private static object ItemVariableValue<T>(T item, string _propertyName)
        {
            object output = item.GetType().GetField(_propertyName) == null ? item : item.GetType().GetField(_propertyName).GetValue(item);
            return output;
        }
        #endregion

        #region IsNullOrEmpty
        /// <summary>
        /// Retorna verdadeiro se a matriz for nula ou vazia
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this T[] data)
        {
            return ((data == null) || (data.Length == 0));
        }

        /// <summary>
        /// Retorna verdadeiro se a lista for nula ou vazia
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this List<T> data)
        {
            return ((data == null) || (data.Count == 0));
        }

        /// <summary>
        /// Retorna verdadeiro se o dicionário for nulo ou vazio
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T1, T2>(this Dictionary<T1, T2> data)
        {
            return ((data == null) || (data.Count == 0));
        }
        #endregion

        #region RemoveDuplicates
        /// <summary>
        /// Remove itens de uma coleção com base na condição fornecida. Isso é útil se uma consulta fornecer
        /// algumas duplicatas das quais você não consegue se livrar. Algumas consultas Linq2Sql são um exemplo disso.
        /// Use este método depois para retirar coisas que você sabe que estão na lista várias vezes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="Predicate"></param>
        /// <remarks>http://extensionmethod.net/csharp/icollection-t/removeduplicates</remarks>
        /// <returns></returns>
        public static IEnumerable<T> RemoveDuplicates<T>(this ICollection<T> list, Func<T, int> Predicate)
        {
            Dictionary<int, T> dict = new Dictionary<int, T>();

            foreach (T item in list)
            {
                if (!dict.ContainsKey(Predicate(item)))
                {
                    dict.Add(Predicate(item), item);
                }
            }

            return dict.Values.AsEnumerable();
        }
        #endregion

        #region DequeueOrNull
        /// <summary>
        /// Deques um item ou retorna nulo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="q"></param>
        /// <returns></returns>
        public static T DequeueOrNull<T>(this Queue<T> q)
        {
            try
            {
                return (q.Count > 0) ? q.Dequeue() : default(T);
            }

            catch (Exception)
            {
                return default(T);
            }
        }
        #endregion

        #region Resize
        public static void Resize<T>(this List<T> List, int NewCount)
        {
            if (NewCount <= 0)
            {
                List.Clear();
            }
            else
            {
                while (List.Count > NewCount) List.RemoveAt(List.Count - 1);
                while (List.Count < NewCount) List.Add(default(T));
            }
        }

        public static void Resize<T>(this List<T> list, int sz, T c = default(T))
        {
            int cur = list.Count;
            if (sz < cur)
            {
                list.RemoveRange(sz, cur - sz);
            }          
            else if (sz > cur)
            {
                list.AddRange(Enumerable.Repeat(c, sz - cur));
            }            
        }
        #endregion

        #region RemoveMissingObjects
        public static void RemoveMissingObjects<T>(this List<T> List)
        {
            for (var i = List.Count - 1; i > -1; i--)
            {
                if (List[i] == null)
                {
                    List.RemoveAt(i);
                }   
            }
        }
        #endregion

        #region GetListType
        public static Type GetListType(this Type source)
        {
            Type innerType = null;

            if (source.IsArray)
            {
                innerType = source.GetElementType();
            }
            else if (source.GetGenericArguments().Any())
            {
                innerType = source.GetGenericArguments()[0];
            }

            return innerType;
        }
        #endregion
    }
}