using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Consonants = ConsonantCollection.DefaultConsonants;
            Vowels = VowelCollection.AllVowels;
        }

        public PhonemeCollection(List<Predicate<Consonant>> consonantsToAdd, List<Predicate<Vowel>> vowelsToAdd)
        {
            Consonants = new ConsonantCollection(consonantsToAdd);
            Vowels = new VowelCollection(vowelsToAdd);
        }
    }
}
