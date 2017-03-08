using System;
using System.Collections.Generic;
using System.Linq;

namespace yod.Phonology
{
    public class LanguagePhonology
    {
        public SyllableStructure SyllableStructure;
        public PhonemeCollection Phonemes;

        // todo: choose random onset and coda consonants

        public List<Consonant> OnsetConsonants
        {
            get { return _onsetConsonants; }
            set
            {
                Predicate<Consonant> test = x => !Phonemes.Consonants.Contains(x);
                if (value.Exists(test)) throw new Exception("Onset consonants list contains phoneme /" + value.Find(test).Symbol + "/ which is not in the phonology.");
                _onsetConsonants = value;
            }
        }

        public List<Consonant> CodaConsonants
        {
            get { return _codaConsonants; }
            set
            {
                Predicate<Consonant> test = x => !Phonemes.Consonants.Contains(x);
                if (value.Exists(test)) throw new Exception("Coda consonants list contains phoneme /" + value.Find(test).Symbol + "/ which is not in the phonology.");
                _codaConsonants = value;
            }
        }

        private List<Consonant> _onsetConsonants;
        public List<Consonant> _codaConsonants;

        // todo: allow for weighted word lengths
        public int WordLengthMin = 1;
        public int WordLengthMax = 3;

        public bool GeminateConsonants;
        public bool LongVowels;

        public LanguagePhonology() : this(
            new SyllableStructure
            (
                onsetStructure: new List<SyllableStructure.SyllableStructureOption>
                {
                    new SyllableStructure.SyllableStructureOption(1, 1)
                },
                nucleusStructure: new List<SyllableStructure.SyllableStructureOption>
                {
                    new SyllableStructure.SyllableStructureOption(1, 1)
                },
                codaStructure: new List<SyllableStructure.SyllableStructureOption>
                {
                    new SyllableStructure.SyllableStructureOption(0, 1)
                }
            ))
        {
        }

        public StressLocation StressLocation;

        // todo: load phonology from json file

        public LanguagePhonology(SyllableStructure sylStructure)
        {
            SyllableStructure = sylStructure;
            Phonemes = new PhonemeCollection();
            OnsetConsonants = Phonemes.Consonants;
            CodaConsonants = Phonemes.Consonants;
        }

        public LanguagePhonology(SyllableStructure sylStructure, List<Predicate<Consonant>> consonantsToAdd, List<Predicate<Vowel>> vowelsToAdd)
        {
            SyllableStructure = sylStructure;
            Phonemes = new PhonemeCollection(consonantsToAdd, vowelsToAdd);
            OnsetConsonants = Phonemes.Consonants;
            CodaConsonants = Phonemes.Consonants;
        }

        public LanguagePhonology(SyllableStructure sylStructure, PhonemeCollection phonemes)
        {
            SyllableStructure = sylStructure;
            Phonemes = phonemes;
            OnsetConsonants = Phonemes.Consonants;
            CodaConsonants = Phonemes.Consonants;
        }

        public LanguagePhonology(SyllableStructure sylStructure, List<Predicate<Consonant>> consonantsToAdd, List<Predicate<Vowel>> vowelsToAdd, List<Consonant> onsetConsonants, List<Consonant> codaConsonants) : this(sylStructure, consonantsToAdd, vowelsToAdd)
        {
            OnsetConsonants = onsetConsonants;
            CodaConsonants = codaConsonants;
        }

        public Syllable GetSyllable()
        {
            return new Syllable(this);
        }

        public Word GetWord()
        {
            return new Word(this);
        }

        public Word GetWord(int syllableLength)
        {
            return new Word(this, syllableLength);
        }

        public static int GetCharacterLength(string s)
        {
            var s2 = s.Replace("\u0316", "").Replace(('͡' + ""), "");
            return s2.Length;
        }

        public static LanguagePhonology Generate()
        {
            var syllStructure = SyllableStructure.Generate();
            var phonemes = PhonemeCollection.Generate();

            var phonology = new LanguagePhonology(syllStructure, phonemes);

            phonology.WordLengthMin = Globals.Random.Next(100) > 75 ? 2 : 1;
            phonology.WordLengthMax = Globals.Random.Next(phonology.WordLengthMin, phonology.WordLengthMin + 4);

            var stressDict = new Dictionary<StressLocation, float>
            {
                {StressLocation.Initial, 25f},
                {StressLocation.Second, 25f},
                {StressLocation.Penultimate, 25f},
                {StressLocation.Ultimate, 25f}
            };

            phonology.StressLocation = Globals.WeightedRandom(stressDict);

            phonology.GeminateConsonants = Globals.Random.Next(100) > 50;
            phonology.LongVowels = Globals.Random.Next(100) > 50;

            if (Globals.Random.Next(100) > 50)
            {
                var num = Math.Max(3, Globals.Random.Next(phonology.Phonemes.Consonants.Count));
                var stack = new Stack<Consonant>(new List<Consonant>(phonology.Phonemes.Consonants).OrderBy(x => Globals.Random.Next()));
                for (var i = 0; i < num; i++)
                {
                    phonology.OnsetConsonants.Add(stack.Pop());
                }
            }
            if (Globals.Random.Next(100) > 50)
            {
                var num = Math.Max(3, Globals.Random.Next(phonology.Phonemes.Consonants.Count));
                var stack = new Stack<Consonant>(new List<Consonant>(phonology.Phonemes.Consonants).OrderBy(x => Globals.Random.Next()));
                for (var i = 0; i < num; i++)
                {
                    phonology.CodaConsonants.Add(stack.Pop());
                }
            }

            return phonology;
        }
    }
}