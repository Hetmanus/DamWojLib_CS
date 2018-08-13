using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace DamWojLib
{
    public static class GeneralExtensionMethods
    {
        public static string GetDescription(this ICustomAttributeProvider type)
        {
            DescriptionAttribute attr = type.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            if (attr != null)
            {
                return attr.Description;
            }
            else
            {
                if (type is FieldInfo)
                {
                    return ((FieldInfo)type).Name.Replace(Consts.spaceSymbolPlaceholder, ' ');
                }
                return type.ToString().Replace(Consts.spaceSymbolPlaceholder, ' ');
            }
        }

        public static bool HasValue<T>(this IEnumerable<T> obj)
        {
            return obj != null && obj.GetEnumerator().MoveNext();
        }
        public static bool HasValue<T>(this IEnumerable<T> obj, Func<T, bool> predicate)
        {
            if (obj.HasValue())
            {
                return obj.Any(predicate);
            }
            return false;
        }

        public static targetT[][] Cast2D<sourceT, targetT>(this Array array)
        {
            targetT[][] answer = new targetT[array.GetLength(0)][];
            for (int row = 0; row < array.GetLength(0); row++)
            {
                answer[row] = ((sourceT[])array.GetValue(row)).Cast<targetT>().ToArray();
            }
            return answer;
        }

        public static MemoryStream BinaryFormatterSerialize<T>(this T data)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            object toSerialize = data;
            if (typeof(T) == typeof(KeyCode[][]))
            {
                toSerialize = ((KeyCode[][])(object)data).Cast2D<KeyCode, int>();
            }

            var answer = new MemoryStream();
            var formater = new BinaryFormatter();
            formater.Serialize(answer, toSerialize);
            return answer;
        }
        public static T BinaryFormatterDeserialize<T>(this MemoryStream data)
        {
            if (typeof(T) == typeof(KeyCode[][]))
            {
                return (T)(object)((int[][])(object)data.ToArray()).Cast2D<int, KeyCode>();
            }
            object answer = new BinaryFormatter().Deserialize(data);
            return (T)answer;
        }

        public static IEnumerable<T> CastToEnumerable<T>(this T content)
        {
            yield return content;
        }
        public static string ContentToString2D<T>(this T[][] data, string columnSeparator = "&", string rowSeparator = "|")
        {
            string answer = string.Empty;
            if (data.HasValue())
            {
                answer += data[0][0].ToString();
                for (int j = 1; j < data[0].Length; j++)
                {
                    answer += columnSeparator;
                    answer += data[0][j].ToString();
                }
                for (int i = 1; i < data.GetLength(0); i++)
                {
                    answer += rowSeparator;
                    answer += data[i][0].ToString();
                    for (int j = 1; j < data[i].Length; j++)
                    {
                        answer += columnSeparator;
                        answer += data[i][j].ToString();
                    }
                }
            }
            return answer;
        }
        public static T[][] StringToContent2D<T>(this string str, Func<string, T> parseMethod, string columnSeparator = "&", string rowSeparator = "|")
        {
            string[] rows = str.Split(new string[] { rowSeparator }, StringSplitOptions.None);
            T[][] answer = new T[rows.Length][];

            for (int i = 0; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(new string[] { columnSeparator }, StringSplitOptions.None);
                answer[i] = new T[columns.Length];
                for (int j = 0; j < columns.Length; j++)
                {
                    answer[i][j] = parseMethod(columns[j]);
                }
            }

            return answer;
        }

    }
}
