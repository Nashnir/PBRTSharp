namespace PBRTSharp.Core
{
    static class ExtraMath
    {
        public static double Lerp(double t, double from, double to) => ((1 - t) * from) + (t * to);
    }
}
