using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace yod.Phonology
{
    /// <summary>
    /// Represents a collection of vowel phonemes.
    /// </summary>
    public class VowelCollection : List<Vowel>
    {
        /// <summary>
        /// A list of every vowel in the Internation Phonetic Alphabet. <see cref="AllVowels"/> for a <see cref="VowelCollection"/> containing all of these vowels.
        /// </summary>
        public static readonly List<Vowel> IPAVowels = new List<Vowel>
        {
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

        /// <summary>
        /// All vowels in the Internation Phonetic Alphabet.
        /// </summary>
        public static VowelCollection AllVowels => new VowelCollection(new List<Predicate<Vowel>> {x => true});

        /// <summary>
        /// All vowels used in English.
        /// </summary>
        /// <remarks>Useful for generating words that will be more pronouncable for most people. However, the idiomatic way to create
        /// a <see cref="PhonemeCollection"/> for English phonemes would be to load one from a JSON file using <see cref="LanguagePhonology.FromJSON(string)"/></remarks>
        public static VowelCollection EnglishVowels => new VowelCollection(new List<Predicate<Vowel>> {x => "iɪuʊeɜəɔæʌɑɒ".Contains(x.Symbol)});

        /// <summary>
        /// The vowels used when creating a <see cref="VowelCollection"/> with the default constructor.
        /// </summary>
        public static VowelCollection DefaultVowels => EnglishVowels;

        /// <summary>
        /// Create a new VowelCollection with the default set of vowels (<see cref="DefaultVowels"/>).
        /// </summary>
        public VowelCollection() : base(DefaultVowels)
        {
        }

        /// <summary>
        /// Create a new VowelCollection containing all vowels that match any of a given set of predicates.
        /// </summary>
        /// <param name="add">A list of predicates to check.</param>
        public VowelCollection(List<Predicate<Vowel>> add)
        {
            add.ForEach(pred => { AddRange(IPAVowels.Where(v => pred(v))); });
        }

        /// <summary>
        /// Create a new VowelCollection containing all vowels from a list.
        /// </summary>
        /// <param name="vowels">A list of vowels to add.</param>
        public VowelCollection(List<Vowel> vowels)
        {
            vowels.ForEach(x => Add(x));
        }

        /// <summary>
        /// Get a random vowel from the collection.
        /// </summary>
        /// <returns>A random vowel.</returns>
        public Vowel GetRandom()
        {
            return new Vowel(this[Globals.Random.Next(Count)]);
        }

        /// <summary>
        /// Generate a random VowelCollection using realistic statistics for picking numbers of vowels.
        /// </summary>
        /// <returns>A random VowelCollection.</returns>
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

        /// <summary>
        /// Generate a random VowelCollection that is a subset of another collection.
        /// </summary>
        /// <param name="collection">The collection to take a subset of. Must contain at least 2 or more vowels.</param>
        /// <returns>A random VowelCollection.</returns>
        /// <remarks></remarks>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static VowelCollection GenerateSubset(VowelCollection collection)
        {
            if (collection.Count < 2) throw new ArgumentOutOfRangeException("Vowel collection must have 2 or more vowel in order to generate subset.");
            var num = Math.Max(2, Globals.Random.Next(collection.Count));
            var stack = new Stack<Vowel>(collection.OrderBy(x => Globals.Random.Next()));
            var list = new List<Vowel>();
            for (var i = 0; i < num; i++)
            {
                list.Add(stack.Pop());
            }
            return new VowelCollection(list);
        }

        /// <summary>
        /// Serializes the collection to JSON.
        /// </summary>
        /// <returns>A JToken representing the collection.</returns>
        public JToken ToJSON()
        {
            return new JArray(this.Select(x => x.Symbol));
        }

        /// <summary>
        /// Deserializes a JSON object into a VowelCollection.
        /// </summary>
        /// <param name="jToken">A JToken representing a VowelCollection</param>
        /// <returns>The deserialized collection.</returns>
        public static VowelCollection FromJSON(JToken jToken)
        {
            var a = (jToken as JArray).ToList().Select(x => x.Value<string>());
            var vowels = IPAVowels.Where(x => a.Contains(x.Symbol)).ToList();
            VowelCollection v = new VowelCollection(vowels);
            return v;
        }
    }
}