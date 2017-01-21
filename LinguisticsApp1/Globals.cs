using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yodWords
{
    static class Globals
    {
        public static Random Random = new Random();
        public static bool ShowIPA = false;

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
    }
}
