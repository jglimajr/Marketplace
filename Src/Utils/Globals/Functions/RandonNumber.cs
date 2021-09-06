using System;
using InteliSystem.Utils.Extensions;

namespace InteliSystem.Utils.Globals.Functions
{
    public static class RandonNumber
    {
        public static int Generete(int minval = 0, int maxval = 99999999, int seed = 99999999)
        {
            if (maxval < seed)
                seed = maxval;
            var ran = new Random(seed);
            return ran.Next(minval, maxval);
        }

        public static string GenereteToString(int minval = 0, int maxval = 99999999, int seed = 99999999)
        {
            var ret = Generete(minval, maxval, seed);
            return ret.ZeroLeft(maxval.ToString().Length);
        }
    }
}