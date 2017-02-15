using System;
using System.Collections.Generic;
using System.Linq;

namespace yod
{
    public static class Globals
    {
        public static Random Random = new Random();

        public static void SeedRandom(int seed)
        {
            Random = new Random(seed);
        }

        public static T WeightedRandom<T>(Dictionary<T, float> options)
        {
            if (options.Count == 0) throw new Exception("Cannot get a random weighted value from an empty list");
            var sum = 0f;
            options.Values.ToList().ForEach(x => sum += x);

            var r = Random.NextDouble() * sum;

            foreach(var kp in options)
            {
                r -= kp.Value;
                if (r <= 0) return kp.Key;
            }

            throw new Exception("Weighted Random failed to return a value somehow");
        }

        public static string StripTies(string ipa)
        {
            return ipa.Replace("\u0361", "");
        }
    }
}
