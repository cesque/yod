using System;
using System.Collections.Generic;

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
            Consonants = new ConsonantCollection(); //ConsonantCollection.Generate();
            Vowels = new VowelCollection();
        }

        // todo: choose random phonemes

        public PhonemeCollection(List<Predicate<Consonant>> consonantsToAdd, List<Predicate<Vowel>> vowelsToAdd)
        {
            Consonants = new ConsonantCollection(consonantsToAdd);
            Vowels = new VowelCollection(vowelsToAdd);
        }
    }
}
