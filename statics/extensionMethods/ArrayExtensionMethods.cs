namespace DamWojLib.ArrayExtensions
{
    public static class ArrayExtensionMethods
    {
        public static string CastToString(this char[] array)
        {
            return new string(array);
        }
        public static string CastToString(this char[] array, int length)
        {
            return new string(array, 0, length);
        }
        public static string CastToString(this char[] array, int startIndex, int length)
        {
            return new string(array, startIndex, length);
        }
    }
}