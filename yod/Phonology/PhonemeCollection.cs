﻿using System;
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
            pc.Consonants = ConsonantCollection.DefaultConsonants;
            pc.Vowels = VowelCollection.DefaultVowels;

            return pc;
        }
    }
}
