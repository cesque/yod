using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace yod
{
    public static class Globals
    {
        public static Random Random = new Random();

        public static int Seed
        {
            get { return _seed; }
            set
            {
                _seed = value;
                Random = new Random(_seed);
            }
        }

        private static int _seed = 0;

        static Globals()
        {
            Random seedGen = new Random();
            Seed = seedGen.Next(0, Int32.MaxValue);
        }

        public static T WeightedRandom<T>(Dictionary<T, float> options)
        {
            if (options.Count == 0) throw new ArgumentException("Cannot get a random weighted value from an empty list");
            var sum = 0f;
            options.Values.ToList().ForEach(x => sum += x);

            var r = Random.NextDouble() * sum;

            foreach (var kp in options)
            {
                r -= kp.Value;
                if (r <= 0) return kp.Key;
            }

            throw new InvalidOperationException("Weighted Random failed to return a value somehow");
        }

        public static string StripTies(string ipa)
        {
            return ipa.Replace("\u0361", "");
        }

        public static string StripCombining(string word)
        {
            var s = "";
            foreach (var c in word)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    s += c;
                }
            }
            return s;
        }

        public static string ConvertToSmallCaps(string input)
        {
            var smallcaps = "ᴀʙᴄᴅᴇꜰɢʜɪᴊᴋʟᴍɴᴏᴘQʀꜱᴛᴜᴠᴡXʏᴢ";
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var s = "";
            foreach (var c in input)
            {
                var capsChar = Char.ToUpper(c);
                if (alphabet.Contains(capsChar))
                {
                    s += smallcaps[alphabet.IndexOf(capsChar)];
                }
                else
                {
                    s += capsChar;
                }
            }
            return s;
        }
    }
}