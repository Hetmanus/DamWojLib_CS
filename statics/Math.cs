
namespace DamWojLib
{
    public static class Math
    {
        public static int Restrict(int minBound, int value, int maxValue)
        {
            return System.Math.Max(minBound, System.Math.Min(maxValue, value));
        }
        public static float Restrict(float minBound, float value, float maxValue)
        {
            return System.Math.Max(minBound, System.Math.Min(maxValue, value));
        }
    }
}
