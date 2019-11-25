using System;

namespace MSIAfterburnerNET.Common.Extensions
{
    public static class MathExtensions
    {
        public static bool IsAlmostEqual(this float a, float b, float epsilon)
        {
            if (a == b)
                return true;

            float aa = Math.Abs(a);
            float bb = Math.Abs(b);

            return a == 0.0f || b == 0.0f || aa + bb < 3.40282346638529E+38
                ? Math.Abs(a - b) < epsilon * 3.40282346638529E+38
                : Math.Abs(a - b) / (aa + bb) < epsilon;
        }
    }
}