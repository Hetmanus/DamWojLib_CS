using System.Collections.Generic;

namespace DamWojLib.BinaryExtensions
{
    public static class BinaryExtensions
    {
        public static short ToShort(this IEnumerator<byte> enumerator)
        {
            enumerator.MoveNext(); short answer = enumerator.Current;
            enumerator.MoveNext(); answer |= (short)(enumerator.Current << 8);
            return answer;
        }
        public static ushort ToUShort(this IEnumerator<byte> enumerator)
        {
            enumerator.MoveNext(); ushort answer = enumerator.Current;
            enumerator.MoveNext(); answer |= (ushort)(enumerator.Current << 8);
            return answer;
        }
        public static int ToInt(this IEnumerator<byte> enumerator)
        {
            enumerator.MoveNext(); int answer = enumerator.Current;
            enumerator.MoveNext(); answer |= enumerator.Current << 8;
            enumerator.MoveNext(); answer |= enumerator.Current << 16;
            enumerator.MoveNext(); answer |= enumerator.Current << 24;
            return answer;
        }
        public static uint ToUInt(this IEnumerator<byte> enumerator)
        {
            enumerator.MoveNext(); uint answer = enumerator.Current;
            enumerator.MoveNext(); answer |= ((uint)enumerator.Current) << 8;
            enumerator.MoveNext(); answer |= ((uint)enumerator.Current) << 16;
            enumerator.MoveNext(); answer |= ((uint)enumerator.Current) << 24;
            return answer;
        }
        public static long ToLong(this IEnumerator<byte> enumerator)
        {
            enumerator.MoveNext(); long answer = enumerator.Current;
            enumerator.MoveNext(); answer |= (long)enumerator.Current << 8;
            enumerator.MoveNext(); answer |= (long)enumerator.Current << 16;
            enumerator.MoveNext(); answer |= (long)enumerator.Current << 24;
            enumerator.MoveNext(); answer |= (long)enumerator.Current << 32;
            enumerator.MoveNext(); answer |= (long)enumerator.Current << 40;
            enumerator.MoveNext(); answer |= (long)enumerator.Current << 48;
            enumerator.MoveNext(); answer |= (long)enumerator.Current << 56;
            return answer;
        }
        public static ulong ToULong(this IEnumerator<byte> enumerator)
        {
            enumerator.MoveNext(); ulong answer = enumerator.Current;
            enumerator.MoveNext(); answer |= (ulong)enumerator.Current << 8;
            enumerator.MoveNext(); answer |= (ulong)enumerator.Current << 16;
            enumerator.MoveNext(); answer |= (ulong)enumerator.Current << 24;
            enumerator.MoveNext(); answer |= (ulong)enumerator.Current << 32;
            enumerator.MoveNext(); answer |= (ulong)enumerator.Current << 40;
            enumerator.MoveNext(); answer |= (ulong)enumerator.Current << 48;
            enumerator.MoveNext(); answer |= (ulong)enumerator.Current << 56;
            return answer;
        }

        public static IEnumerable<byte> ToBinary(this short data)
        {
            yield return (byte)(data >> 0 & 255);
            yield return (byte)(data >> 8 & 255);
        }
        public static IEnumerable<byte> ToBinary(this ushort data)
        {
            yield return (byte)(data >> 0 & 255);
            yield return (byte)(data >> 8 & 255);
        }
        public static IEnumerable<byte> ToBinary(this int data)
        {
            yield return (byte)(data >> 0 & 255);
            yield return (byte)(data >> 8 & 255);
            yield return (byte)(data >> 16 & 255);
            yield return (byte)(data >> 24 & 255);
        }
        public static IEnumerable<byte> ToBinary(this uint data)
        {
            yield return (byte)(data >> 0 & 255);
            yield return (byte)(data >> 8 & 255);
            yield return (byte)(data >> 16 & 255);
            yield return (byte)(data >> 24 & 255);
        }
        public static IEnumerable<byte> ToBinary(this long data)
        {
            yield return (byte)(data >> 0 & 255);
            yield return (byte)(data >> 8 & 255);
            yield return (byte)(data >> 16 & 255);
            yield return (byte)(data >> 24 & 255);
            yield return (byte)(data >> 32 & 255);
            yield return (byte)(data >> 40 & 255);
            yield return (byte)(data >> 48 & 255);
            yield return (byte)(data >> 56 & 255);
        }
        public static IEnumerable<byte> ToBinary(this ulong data)
        {
            yield return (byte)(data >> 0 & 255);
            yield return (byte)(data >> 8 & 255);
            yield return (byte)(data >> 16 & 255);
            yield return (byte)(data >> 24 & 255);
            yield return (byte)(data >> 32 & 255);
            yield return (byte)(data >> 40 & 255);
            yield return (byte)(data >> 48 & 255);
            yield return (byte)(data >> 56 & 255);
        }
    }
}
