using System;
using System.Collections.Generic;

namespace SirenixPowered.ExtensionMethods
{
    public static class ArrayExtension
    {
        /// <summary>
        /// Remove elemento de uma array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="index"></param>
        public static void RemoveAt<T>(this T[] array, out T[] output, int index)
        {
            array[index] = array[array.Length - 1];
            Array.Resize(ref array, array.Length - 1);
            output = array;
        }

        /// <summary>
        /// Add an element to an array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="output"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T[] Add<T>(this T[] array, out T[] output,  T value)
        {
            T[] newArray = new T[array.Length + 1];
            array.CopyTo(newArray, 0);
            newArray[array.Length] = value;

            output = newArray;
            return newArray;
        }

        /// <summary>
        /// Get the number of elements in an array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="output"></param>
        /// <returns></returns>
        public static int Count<T>(this T[] output)
        {
            return output.Length;
        }

        /// <summary>
        /// Remove all elements from an array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="output"></param>
        public static void Clear<T>(this T[] array, out T[] output)
        {
            output = new T[0];
        }

        /// <summary>
        /// Convert an array to list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this T[] array)
        {
            List<T> output = new List<T>();
            output.AddRange(array);
            return output;
        }

        public static void Resize<T>(this T[] array, out T[] output, int newSize)
        {
            Array.Resize(ref array, newSize);
            output = array;
        }
    }
}