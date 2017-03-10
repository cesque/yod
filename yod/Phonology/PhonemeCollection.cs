using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace yod.Phonology
{
    public class PhonemeCollection
    {
        public List<Phoneme> AllPhonemes
        {
            get
            {
                var list = new List<Phoneme>();
                list.AddRange(Consonants);
                list.AddRange(Vowels);
                return list;
            }
        }

        public ConsonantCollection Consonants;
        public VowelCollection Vowels;

        public PhonemeCollection()
        {
            //Consonants = ConsonantCollection.DefaultConsonants;
            Consonants = ConsonantCollection.DefaultConsonants;
            Vowels = VowelCollection.DefaultVowels;
        }

        // todo: choose random phonemes

        public PhonemeCollection(List<Predicate<Consonant>> consonantsToAdd, List<Predicate<Vowel>> vowelsToAdd)
        {
            Consonants = new ConsonantCollection(consonantsToAdd);
            Vowels = new VowelCollection(vowelsToAdd);
        }

        public static PhonemeCollection Generate()
        {
            PhonemeCollection pc = new PhonemeCollection();
            pc.Consonants = ConsonantCollection.Generate();
            pc.Vowels = VowelCollection.Generate();

            return pc;
        }

        public static PhonemeCollection GenerateEnglishSubset()
        {
            PhonemeCollection pc = new PhonemeCollection();
            pc.Consonants = ConsonantCollection.GenerateSubset(ConsonantCollection.EnglishConsonants);
            pc.Vowels = VowelCollection.GenerateSubset(VowelCollection.EnglishVowels);
            while (pc.AllPhonemes.Count <= 5)
            {
                pc.Consonants = ConsonantCollection.GenerateSubset(ConsonantCollection.EnglishConsonants);
                pc.Vowels = VowelCollection.GenerateSubset(VowelCollection.EnglishVowels);
            }

            return pc;
        }

        public static PhonemeCollection GenerateSubset(PhonemeCollection collection)
        {
            if (collection.AllPhonemes.Count <= 5) throw new ArgumentOutOfRangeException("Phoneme collection must have more than 5 phonemes to generate subset.");
            PhonemeCollection pc = new PhonemeCollection();
            pc.Consonants = ConsonantCollection.GenerateSubset(collection.Consonants);
            pc.Vowels = VowelCollection.GenerateSubset(collection.Vowels);
            while (pc.AllPhonemes.Count <= 5)
            {
                pc.Consonants = ConsonantCollection.GenerateSubset(collection.Consonants);
                pc.Vowels = VowelCollection.GenerateSubset(collection.Vowels);
            }

            return pc;
        }

        public JToken ToJSON()
        {
            var o = new JObject();
            o.Add("consonants", Consonants.ToJSON());
            o.Add("vowels", Vowels.ToJSON());
            return o;
        }

        public static PhonemeCollection FromJSON(JToken jToken)
        {
            var o = jToken as JObject;
            PhonemeCollection p = new PhonemeCollection();
            p.Consonants = ConsonantCollection.FromJSON(o["consonants"]);
            p.Vowels = VowelCollection.FromJSON(o["vowels"]);

            if (o["generatesubset"] != null && o["generatesubset"].Value<bool>())
            {
                return GenerateSubset(p);
            }
            else
            {
                return p;
            }
        }
    }
}