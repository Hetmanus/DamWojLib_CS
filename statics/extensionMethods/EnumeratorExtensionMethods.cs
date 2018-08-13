using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DamWojLib.EnumeratorExtensions
{
    public static class EnumeratorExtensions
    {
        public static int IndexOf(this IEnumerator array, Predicate<object> predicate)
        {
            int i = 0;
            while (array.MoveNext())
            {
                if (predicate(array.Current))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
        public static int IndexOf<T>(this IEnumerator<T> array, Predicate<T> predicate)
        {
            int i = 0;
            while (array.MoveNext())
            {
                if (predicate(array.Current))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public static IEnumerator<T> Assert<T>(this IEnumerator<T> enumerator)
        {
            if (enumerator != null)
            {
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }
        public static IEnumerator<T> IncludeCurrent<T>(this IEnumerator<T> enumerator)
        {
            bool curentAvailable = false;
            try
            {
                var foo = enumerator.Current;
                curentAvailable = true;
            }
            catch (Exception e) { Debug.WriteLine(e); }
            if (curentAvailable)
            {
                yield return enumerator.Current;
            }
            else
            {
                yield return default(T);
            }

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerator<T> YieldAtStart<T>(this IEnumerator<T> enumerator, IEnumerator<T> toYield)
        {
            while (toYield.MoveNext())
            {
                yield return toYield.Current;
            }
            if (enumerator != null)
            {
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }
        public static IEnumerator<T> YieldAtStart<T>(this IEnumerator<T> enumerator, T toYield)
        {
            yield return toYield;
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
        public static IEnumerator<T> YieldAtEnd<T>(this IEnumerator<T> enumerator, IEnumerator<T> toYield)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
            while (toYield.MoveNext())
            {
                yield return toYield.Current;
            }
        }
        public static IEnumerator<T> YieldAtEnd<T>(this IEnumerator<T> enumerator, T toYield)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
            yield return toYield;
        }

        public static IEnumerator<T> YieldBefor<T>(this IEnumerator<T> enumerator, Predicate<T> predicate, IEnumerable<T> toYield)
        {
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    foreach (var item in toYield)
                    {
                        yield return item;
                    }
                }
                yield return enumerator.Current;
            }
        }
        public static IEnumerator<T> YieldAfter<T>(this IEnumerator<T> enumerator, Predicate<T> predicate, IEnumerable<T> toYield)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
                if (predicate(enumerator.Current))
                {
                    foreach (var item in toYield)
                    {
                        yield return item;
                    }
                }
            }
        }
        public static IEnumerator<T> YieldBeforAndAfter<T>(this IEnumerator<T> enumerator, Predicate<T> predicate, IEnumerable<T> toYield)
        {
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    foreach (var item in toYield)
                    {
                        yield return item;
                    }
                }
                yield return enumerator.Current;
                if (predicate(enumerator.Current))
                {
                    foreach (var item in toYield)
                    {
                        yield return item;
                    }
                }
            }
        }

        public static IEnumerator<T> Select<T>(this IEnumerator<T> enumerator, Predicate<T> predicate)
        {
            while (enumerator.MoveNext())
            {
                if (predicate(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }
        }
        public static IEnumerator<T> ReplaceSequence<T>(this IEnumerator<T> enumerator, IEnumerable<T> toReplace, IEnumerable<T> replaceWith)
        {
            var fooList = new List<T>();
            IEnumerator<T> toReplaceEnumerator = toReplace.GetEnumerator();
            toReplaceEnumerator.MoveNext();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Equals(toReplaceEnumerator.Current))
                {
                    fooList.Add(enumerator.Current);
                    while (true)
                    {
                        if (toReplaceEnumerator.MoveNext())
                        {
                            if (enumerator.MoveNext())
                            {
                                if (enumerator.Current.Equals(toReplaceEnumerator.Current))
                                {
                                    fooList.Add(enumerator.Current);
                                }
                                else
                                {
                                    foreach (var item in fooList)
                                    {
                                        yield return item;
                                    }
                                    yield return enumerator.Current;
                                    fooList.Clear();
                                    toReplaceEnumerator = toReplace.GetEnumerator();
                                    toReplaceEnumerator.MoveNext();
                                    break;
                                }
                            }
                            else
                            {
                                foreach (var item in fooList)
                                {
                                    yield return item;
                                }
                                fooList.Clear();
                                toReplaceEnumerator = toReplace.GetEnumerator();
                                toReplaceEnumerator.MoveNext();
                                break;
                            }
                        }
                        else
                        {
                            foreach (var item in replaceWith)
                            {
                                yield return item;
                            }
                            fooList.Clear();
                            toReplaceEnumerator = toReplace.GetEnumerator();
                            toReplaceEnumerator.MoveNext();
                            break;
                        }
                    }
                }
                else
                {
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerator<T> GetBefor<T>(this IEnumerator<T> enumerator, Predicate<T> predicate)
        {
            while (enumerator.MoveNext() && !predicate(enumerator.Current))
            {
                yield return enumerator.Current;
            }
        }
        public static IEnumerator<T> GetAfter<T>(this IEnumerator<T> enumerator, Predicate<T> predicate)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
        public static IEnumerator<T> GetEnumerator<T>(this IEnumerable<T> enumerable, int startIndex, int length = int.MaxValue)
        {
            using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
            {
                for (int i = 0; i < startIndex; i++)
                {
                    enumerator.MoveNext();
                }
                while (enumerator.MoveNext() && length > 0)
                {
                    yield return enumerator.Current;
                    length--;
                }
            }
        }
        public static IEnumerator<T> GetEnumerator<T>(this IEnumerable enumerable, int startIndex = 0, int length = int.MaxValue)
        {
            using (IEnumerator<T> enumerator = (enumerable as IEnumerable<T>).GetEnumerator())
            {
                for (int i = 0; i < startIndex; i++)
                {
                    enumerator.MoveNext();
                }
                while (enumerator.MoveNext() && length > 0)
                {
                    yield return enumerator.Current;
                    length--;
                }
            }
        }
        public static IEnumerator<T> Bracketer<T>(this IEnumerator<T> enumerator, T openingBracket, T closingBracket, int bracketsOppened = 0)
        {
            Predicate<T> predicate = c =>
            {
                if (c.Equals(openingBracket))
                {
                    bracketsOppened++;
                }
                if (c.Equals(closingBracket))
                {
                    bracketsOppened--;
                    if (bracketsOppened == 0)
                    {
                        return true;
                    }
                }
                return false;
            };

            while (enumerator.MoveNext() && !predicate(enumerator.Current))
            {
                if (bracketsOppened >= 0)
                {
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerator<char> EncodeControl(this IEnumerator<char> enumeratror)
        {
            byte stringMode = 0;

            while (enumeratror.MoveNext())
            {
                if (stringMode > 0)
                {
                    if (enumeratror.Current == '\\')
                    {
                        stringMode = 2;
                    }
                    else if (enumeratror.Current == '"')
                    {
                        stringMode--;
                    }
                    else
                    {
                        stringMode = 1;
                    }
                    switch (enumeratror.Current)
                    {
                        case '\n': yield return '\\'; yield return 'n'; break;
                        case '\r': yield return '\\'; yield return 'r'; break;
                        case '\t': yield return '\\'; yield return 't'; break;
                        //case '\\': yield return '\\'; yield return '\\'; break;
                        default: yield return enumeratror.Current; break;
                    }
                }
                else
                {
                    if (enumeratror.Current == '"')
                    {
                        stringMode = 1;
                    }
                    yield return enumeratror.Current;
                }
            }
        }
        public static IEnumerator<char> DecodeControl(this IEnumerator<char> enumeratror)
        {
            byte stringMode = 0;

            while (enumeratror.MoveNext())
            {
                if (stringMode > 0)
                {
                    if (enumeratror.Current == '\\')
                    {
                        stringMode = 2;
                    }
                    else if (enumeratror.Current == '"')
                    {
                        stringMode--;
                    }
                    else
                    {
                        stringMode = 1;
                    }
                    if (stringMode == 2)
                    {
                        if (enumeratror.MoveNext())
                        {
                            switch (enumeratror.Current)
                            {
                                case 'n': yield return '\n'; break;
                                case 'r': yield return '\r'; break;
                                case 't': yield return '\t'; break;
                                default: yield return enumeratror.Current; break;
                            }
                        }
                        stringMode = 1;
                    }
                    else
                    {
                        yield return enumeratror.Current;
                    }
                }
                else
                {
                    if (enumeratror.Current == '"')
                    {
                        stringMode = 1;
                    }
                    yield return enumeratror.Current;
                }
            }
        }

        public static T[] ToArray<T>(this IEnumerator<T> enumerator)
        {
            int count = 0;
            T[] array = new T[64];

            while (enumerator.MoveNext())
            {
                if (count >= array.Length)
                {
                    System.Array.Resize(ref array, array.Length * 2);
                }

                array[count] = enumerator.Current;
                count++;
            }
            System.Array.Resize(ref array, count);
            return array;
        }
        public static int ToArrayNonAloc<T>(this IEnumerator<T> enumerator, ref T[] array, int startIndex = 0)
        {
            int count = 0;

            while (enumerator.MoveNext())
            {
                array[startIndex + count] = enumerator.Current;
                count++;
            }
            return count;
        }
        public static object[] ToArray(this IEnumerator enumerator)
        {
            int count = 0;
            object[] array = new object[64];

            while (enumerator.MoveNext())
            {
                if (count >= array.Length)
                {
                    System.Array.Resize(ref array, array.Length * 2);
                }

                array[count] = enumerator.Current;
                count++;
            }
            System.Array.Resize(ref array, count);
            return array;
        }
        public static int ToArrayNonAloc(this IEnumerator enumerator, ref object[] array, int startIndex = 0)
        {
            int count = 0;

            while (enumerator.MoveNext())
            {
                array[startIndex + count] = enumerator.Current;
                count++;
            }
            return count;
        }

        public static string ContentToString(this IEnumerator enumerator, string separator = null)
        {
            var builder = new StringBuilder();
            enumerator.MoveNext();
            builder.Append(enumerator.Current);
            while (enumerator.MoveNext())
            {
                if (separator != null)
                {
                    builder.Append(separator);
                }
                builder.Append(enumerator.Current);
            }
            return builder.ToString();
        }
        public static string ContentToString(this IEnumerator<char> enumerator)
        {
            return new string(enumerator.ToArray());
        }
        public static string ContentToStringNonAloc(this IEnumerator<char> enumerator, ref char[] array)
        {
            int lenght = enumerator.ToArrayNonAloc(ref array);
            return new string(array, 0, lenght);
        }
    }
}
