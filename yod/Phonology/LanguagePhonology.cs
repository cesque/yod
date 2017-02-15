using System;
using System.Collections.Generic;

namespace yod.Phonology
{
    public class LanguagePhonology
    {
        public SyllableStructure SyllableStructure;
        public PhonemeCollection Phonemes;

        public List<Consonant> OnsetConsonants;
        public List<Consonant> CodaConsonants;

        public int WordLengthMin = 1;
        public int WordLengthMax = 3;

        public LanguagePhonology() : this(
            new SyllableStructure
            {
                OnsetStructure = new List<SyllableStructure.SyllableStructureOption>
                {
                    new SyllableStructure.SyllableStructureOption(1,1)
                },
                NucleusStructure = new List<SyllableStructure.SyllableStructureOption>
                {
                    new SyllableStructure.SyllableStructureOption (1,1)
                },
                CodaStructure = new List<SyllableStructure.SyllableStructureOption>
                {
                    new SyllableStructure.SyllableStructureOption (0,1)
                }
            })
        { }

        public LanguagePhonology(SyllableStructure sylStructure)
        {
            SyllableStructure = sylStructure;
            Phonemes = new PhonemeCollection();
        }

        public LanguagePhonology(SyllableStructure sylStructure, List<Predicate<Consonant>> consonantsToAdd, List<Predicate<Vowel>> vowelsToAdd)
        {
            SyllableStructure = sylStructure;
            Phonemes = new PhonemeCollection(consonantsToAdd, vowelsToAdd);
        }

        public LanguagePhonology(SyllableStructure sylStructure, List<Predicate<Consonant>> consonantsToAdd, List<Predicate<Vowel>> vowelsToAdd, List<Consonant> onsetConsonants, List<Consonant> codaConsonants) : this(sylStructure, consonantsToAdd, vowelsToAdd)
        {
            OnsetConsonants = onsetConsonants;
            CodaConsonants = codaConsonants;
            OnsetConsonants.ForEach(c =>
            {
                if(!Phonemes.Consonants.Contains(c))
                {
                    throw new Exception("Onset consonants list contains phoneme /" + c.Symbol + "/ which is not in the phonology.");
                }              
            });
            CodaConsonants.ForEach(c =>
            {
                if (!Phonemes.Consonants.Contains(c))
                {
                    throw new Exception("Coda consonants list contains phoneme /" + c.Symbol + "/ which is not in the phonology.");
                }
            });
        }

        public Syllable GetSyllable()
        {
            return new Syllable(this);
        }

        public Word GetWord()
        {
            return new Word(this);
        }

        public static int GetCharacterLength(string s)
        {
            var s2 = s.Replace("\u0316", "").Replace(('͡' + ""), "");
            return s2.Length;
        }
    }
}
