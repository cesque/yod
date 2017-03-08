using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public StressLocation StressLocation;

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
                phonology.OnsetConsonants = new List<Consonant>();
                var num = Math.Max(3, Globals.Random.Next(phonology.Phonemes.Consonants.Count));
                var stack = new Stack<Consonant>(new List<Consonant>(phonology.Phonemes.Consonants).OrderBy(x => Globals.Random.Next()));
                for (var i = 0; i < num; i++)
                {
                    phonology.OnsetConsonants.Add(stack.Pop());
                }
            }
            if (Globals.Random.Next(100) > 50)
            {
                phonology.CodaConsonants = new List<Consonant>();
                var num = Math.Max(3, Globals.Random.Next(phonology.Phonemes.Consonants.Count));
                var stack = new Stack<Consonant>(new List<Consonant>(phonology.Phonemes.Consonants).OrderBy(x => Globals.Random.Next()));
                for (var i = 0; i < num; i++)
                {
                    phonology.CodaConsonants.Add(stack.Pop());
                }
            }

            return phonology;
        }

        public JToken ToJSON()
        {
            var stressDict = new Dictionary<StressLocation, string>
            {
                {StressLocation.Initial, "initial"},
                {StressLocation.Second, "second"},
                {StressLocation.Penultimate, "penultimate"},
                {StressLocation.Ultimate, "ultimate"},
            };

            JObject o = new JObject();
            o.Add("wordlengthmin", new JValue(WordLengthMin));
            o.Add("wordlengthmax", new JValue(WordLengthMax));
            o.Add("geminateconsonants", new JValue(GeminateConsonants));
            o.Add("longvowels", new JValue(LongVowels));
            o.Add("stresslocation", new JValue(stressDict[StressLocation]));
            o.Add("syllablestructure", SyllableStructure.ToJSON());
            o.Add("phonemes", Phonemes.ToJSON());
            o.Add("onsetconsonants", OnsetConsonants.Count < Phonemes.Consonants.Count ? new JArray(OnsetConsonants.Select(x => x.Symbol)) : new JArray());
            o.Add("codaconsonants", CodaConsonants.Count < Phonemes.Consonants.Count ? new JArray(CodaConsonants.Select(x => x.Symbol)) : new JArray());
            return o;
        }
    }
}