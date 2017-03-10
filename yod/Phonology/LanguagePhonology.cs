using System;
using System.Collections.Generic;
using System.IO;
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
                if (value.Exists(test)) throw new ArgumentException("Onset consonants list contains phoneme /" + value.Find(test).Symbol + "/ which is not in the phonology.");
                _onsetConsonants = value;
            }
        }

        public List<Consonant> CodaConsonants
        {
            get { return _codaConsonants; }
            set
            {
                Predicate<Consonant> test = x => !Phonemes.Consonants.Contains(x);
                if (value.Exists(test)) throw new ArgumentException("Coda consonants list contains phoneme /" + value.Find(test).Symbol + "/ which is not in the phonology.");
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

            phonology.WordLengthMin = GenerateWordLengthMin();
            phonology.WordLengthMax = GenerateWordLengthMax(phonology.WordLengthMin);

            phonology.StressLocation = GenerateStressLocation();

            phonology.GeminateConsonants = GenerateGeminateConsonants();
            phonology.LongVowels = GenerateLongVowel();

            phonology.OnsetConsonants = GenerateOnsetConsonants(phonology.Phonemes.Consonants);
            phonology.CodaConsonants = GenerateCodaConsonants(phonology.Phonemes.Consonants);

            return phonology;
        }

        static private int GenerateWordLengthMin() => Globals.Random.Next(100) > 75 ? 2 : 1;
        static private int GenerateWordLengthMax(int min) => Globals.Random.Next(min, min + 2);

        static private bool GenerateGeminateConsonants() => Globals.Random.Next(100) > 50;
        static private bool GenerateLongVowel() => Globals.Random.Next(100) > 50;

        static private StressLocation GenerateStressLocation()
        {
            var stressDict = new Dictionary<StressLocation, float>
            {
                {StressLocation.Initial, 25f},
                {StressLocation.Second, 25f},
                {StressLocation.Penultimate, 25f},
                {StressLocation.Ultimate, 25f}
            };

            return Globals.WeightedRandom(stressDict);
        }

        static private List<Consonant> GenerateOnsetConsonants(ConsonantCollection consonants)
        {
            if (Globals.Random.Next(100) > 50)
            {
                var oc = new List<Consonant>();
                var num = Math.Max(2, Globals.Random.Next(consonants.Count));
                var stack = new Stack<Consonant>(new List<Consonant>(consonants).OrderBy(x => Globals.Random.Next()));
                for (var i = 0; i < num; i++)
                {
                    oc.Add(stack.Pop());
                }
                return oc;
            }
            else
            {
                return consonants;
            }
        }

        static private List<Consonant> GenerateCodaConsonants(ConsonantCollection consonants)
        {
            if (Globals.Random.Next(100) > 50)
            {
                var oc = new List<Consonant>();
                var num = Math.Max(2, Globals.Random.Next(consonants.Count));
                var stack = new Stack<Consonant>(new List<Consonant>(consonants).OrderBy(x => Globals.Random.Next()));
                for (var i = 0; i < num; i++)
                {
                    oc.Add(stack.Pop());
                }
                return oc;
            }
            else
            {
                return consonants;
            }
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

        public static LanguagePhonology FromJSON(JToken jtoken)
        {
            var stressDict = new Dictionary<string, StressLocation>
            {
                {"initial", StressLocation.Initial},
                {"second", StressLocation.Second},
                {"penultimate", StressLocation.Penultimate},
                {"ultimate", StressLocation.Ultimate},
            };

            var o = jtoken as JObject;

            var phonology = new LanguagePhonology();

            phonology.WordLengthMin = o["wordlengthmin"] == null ? GenerateWordLengthMin() : (int) o["wordlengthmin"];
            phonology.WordLengthMax = o["wordlengthmax"] == null ? GenerateWordLengthMax(phonology.WordLengthMin) : (int) o["wordlengthmax"];
            if (phonology.WordLengthMin > phonology.WordLengthMax) throw new ArgumentOutOfRangeException("Word length minimum must be smaller or equal to word length max.");
            phonology.GeminateConsonants = o["geminateconsonants"] == null ? GenerateGeminateConsonants() : (bool) o["geminateconsonants"];
            phonology.LongVowels = o["longvowels"] == null ? GenerateLongVowel() : (bool) o["longvowels"];
            phonology.StressLocation = o["stresslocation"] == null ? GenerateStressLocation() : stressDict[(string) o["stresslocation"]];
            phonology.SyllableStructure = o["syllablestructure"] == null ? SyllableStructure.Generate() : SyllableStructure.FromJSON(o["syllablestructure"]);
            phonology.Phonemes = o["phonemes"] == null ? PhonemeCollection.Generate() : PhonemeCollection.FromJSON(o["phonemes"]);

            if (o["onsetconsonants"] == null)
            {
                phonology.OnsetConsonants = GenerateOnsetConsonants(phonology.Phonemes.Consonants);
            }
            else
            {
                var oc = (o["onsetconsonants"] as JArray).Select(x => x.Value<string>()).ToList();
                var onsetCons = ConsonantCollection.IPAConsonants.Where(x => oc.Contains(x.Symbol)).ToList();
                phonology.OnsetConsonants = onsetCons.Count == 0 ? phonology.Phonemes.Consonants : onsetCons;
            }

            if (o["codaconsonants"] == null)
            {
                phonology.CodaConsonants = GenerateCodaConsonants(phonology.Phonemes.Consonants);
            }
            else
            {
                var cc = (o["codaconsonants"] as JArray).Select(x => x.Value<string>()).ToList();
                var codaCons = ConsonantCollection.IPAConsonants.Where(x => cc.Contains(x.Symbol)).ToList();
                phonology.CodaConsonants = codaCons.Count == 0 ? phonology.Phonemes.Consonants : codaCons;
            }

            return phonology;
        }

        public static LanguagePhonology FromJSON(string path)
        {
            return FromJSON(JToken.Parse(File.ReadAllText(path)));
        }
    }
}