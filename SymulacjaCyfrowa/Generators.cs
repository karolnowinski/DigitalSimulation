using System;
using System.Linq;
using System.IO;

namespace SymulacjaCyfrowa
{
    public static class Generators
    {
        const int Q = 127773;
        const int R = 2836;
        const int Range = 2147483647; //2^31-1


        public static double Uniform(ref int x) // from 0 to 1
        {
            int h = (x / Q);
            x = 16807 * (x - Q * h) - R * h;
            if (x < 0) x += Range;
            double ret = x / ((double)Range);
            return ret;
        }

        public static double Exponential(double lambda, ref int x)
        {
            lambda = 1 / lambda;
            double ret = -Math.Log(Uniform(ref x)) / lambda;
            return ret;
        }

        public static double Normal(ref int x, ref int x1, int mean, int variance)
        {
            double s;

            if (Uniform(ref x) > 0.50) s = 1.0;
            else s = -1.0;

            double r = Uniform(ref x1);
            return s * ((Math.Sqrt(variance)) * Math.Sqrt((Math.PI) / 8)) * Math.Log((1 + r) / (1 - r)) + mean;
        }

    }
}
