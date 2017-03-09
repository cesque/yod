using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace yod.Phonology
{
    public class VowelCollection : List<Vowel>
    {
        public static readonly List<Vowel> IPAVowels = new List<Vowel>
        {
            // todo: add more vowels
            new Vowel("i", Vowel.Height.Close, Vowel.Backness.Front, Vowel.Rounding.Unrounded, sonority: 2),
            new Vowel("y", Vowel.Height.Close, Vowel.Backness.Front, Vowel.Rounding.Rounded, sonority: 2),
            new Vowel("ɨ", Vowel.Height.Close, Vowel.Backness.Central, Vowel.Rounding.Unrounded, sonority: 2),
            new Vowel("ʉ", Vowel.Height.Close, Vowel.Backness.Central, Vowel.Rounding.Rounded, sonority: 2),
            new Vowel("ɯ", Vowel.Height.Close, Vowel.Backness.Back, Vowel.Rounding.Unrounded, sonority: 2),
            new Vowel("u", Vowel.Height.Close, Vowel.Backness.Back, Vowel.Rounding.Rounded, sonority: 2),
            new Vowel("ɪ", Vowel.Height.NearClose, Vowel.Backness.NearFront, Vowel.Rounding.Unrounded, 2),
            new Vowel("ʏ", Vowel.Height.NearClose, Vowel.Backness.NearFront, Vowel.Rounding.Rounded, 2),
            new Vowel("ɪ̈", Vowel.Height.NearClose, Vowel.Backness.Central, Vowel.Rounding.Unrounded, 2),
            new Vowel("ʊ̈", Vowel.Height.NearClose, Vowel.Backness.Central, Vowel.Rounding.Rounded, 2),
            new Vowel("ʊ", Vowel.Height.NearClose, Vowel.Backness.NearBack, Vowel.Rounding.Rounded, 2),
            new Vowel("e", Vowel.Height.CloseMid, Vowel.Backness.Front, Vowel.Rounding.Unrounded, 1),
            new Vowel("ø", Vowel.Height.CloseMid, Vowel.Backness.Front, Vowel.Rounding.Rounded, 1),
            new Vowel("ɘ", Vowel.Height.CloseMid, Vowel.Backness.Central, Vowel.Rounding.Unrounded, 1),
            new Vowel("ɵ", Vowel.Height.CloseMid, Vowel.Backness.Central, Vowel.Rounding.Rounded, 1),
            new Vowel("ɤ", Vowel.Height.CloseMid, Vowel.Backness.Back, Vowel.Rounding.Unrounded, 1),
            new Vowel("o", Vowel.Height.CloseMid, Vowel.Backness.Back, Vowel.Rounding.Rounded, 1),
            new Vowel("ə", Vowel.Height.Mid, Vowel.Backness.Central, Vowel.Rounding.Unrounded, 1),
            new Vowel("ɛ", Vowel.Height.OpenMid, Vowel.Backness.Front, Vowel.Rounding.Unrounded, 0),
            new Vowel("œ", Vowel.Height.OpenMid, Vowel.Backness.Front, Vowel.Rounding.Rounded, 0),
            new Vowel("ɜ", Vowel.Height.OpenMid, Vowel.Backness.Central, Vowel.Rounding.Unrounded, 0),
            new Vowel("ɞ", Vowel.Height.OpenMid, Vowel.Backness.Central, Vowel.Rounding.Rounded, 0),
            new Vowel("ʌ", Vowel.Height.OpenMid, Vowel.Backness.Back, Vowel.Rounding.Unrounded, 0),
            new Vowel("ɔ", Vowel.Height.OpenMid, Vowel.Backness.Back, Vowel.Rounding.Rounded, 0),
            new Vowel("æ", Vowel.Height.NearOpen, Vowel.Backness.Front, Vowel.Rounding.Unrounded, 0),
            new Vowel("ɐ", Vowel.Height.NearOpen, Vowel.Backness.Central, Vowel.Rounding.Unrounded, 0),
            new Vowel("a", Vowel.Height.Open, Vowel.Backness.Front, Vowel.Rounding.Unrounded, 0),
            new Vowel("ɶ", Vowel.Height.Open, Vowel.Backness.Front, Vowel.Rounding.Rounded, 0),
            new Vowel("ä", Vowel.Height.Open, Vowel.Backness.Central, Vowel.Rounding.Unrounded, 0),
            new Vowel("ɑ", Vowel.Height.Open, Vowel.Backness.Back, Vowel.Rounding.Unrounded, 0),
            new Vowel("ɒ", Vowel.Height.Open, Vowel.Backness.Back, Vowel.Rounding.Rounded, 0),
        };

        public static VowelCollection AllVowels => new VowelCollection();

        public static VowelCollection EnglishVowels => new VowelCollection(new List<Predicate<Vowel>> {x => "iɪuʊeɜəɔæʌɑɒ".Contains(x.Symbol)});

        public static VowelCollection DefaultVowels => EnglishVowels;

        public VowelCollection() : base(EnglishVowels)
        {
        }

        public VowelCollection(List<Predicate<Vowel>> add)
        {
            add.ForEach(pred => { AddRange(IPAVowels.Where(v => pred(v))); });
        }

        public VowelCollection(List<Vowel> vowels)
        {
            vowels.ForEach(x => Add(x));
        }

        public Vowel GetRandom()
        {
            return this[Globals.Random.Next(Count)];
        }

        public static VowelCollection Generate()
        {
            var probabilities = new Dictionary<Tuple<int, int>, float>
            {
                {new Tuple<int, int>(2, 4), 93f},
                {new Tuple<int, int>(5, 6), 287f},
                {new Tuple<int, int>(7, 14), 184f}
            };

            var vowelProbability = Globals.WeightedRandom(probabilities);
            var vowelCount = Globals.Random.Next(vowelProbability.Item1, vowelProbability.Item2);

            var collection = new List<Vowel>();
            for (var i = 0; i < vowelCount; i++)
            {
                var random = IPAVowels[Globals.Random.Next(IPAVowels.Count)];
                while (collection.Contains(random)) random = IPAVowels[Globals.Random.Next(IPAVowels.Count)];
                collection.Add(random);
            }

            return new VowelCollection(collection);
        }

        public static VowelCollection GenerateSubset(VowelCollection collection)
        {
            var num = Math.Max(2, Globals.Random.Next(collection.Count));
            var stack = new Stack<Vowel>(collection.OrderBy(x => Globals.Random.Next()));
            var list = new List<Vowel>();
            for (var i = 0; i < num; i++)
            {
                list.Add(stack.Pop());
            }
            return new VowelCollection(list);
        }

        public JToken ToJSON()
        {
            return new JArray(this.Select(x => x.Symbol));
        }

        public static VowelCollection FromJSON(JToken jToken)
        {
            var a = (jToken as JArray).ToList().Select(x => x.Value<string>());
            var vowels = IPAVowels.Where(x => a.Contains(x.Symbol)).ToList();
            VowelCollection v = new VowelCollection(vowels);
            return v;
        }
    }
}