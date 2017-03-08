using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace yod.Phonology
{
    public class ConsonantCollection : List<Consonant>
    {
        /*
                |BL   |LD   |D    |AV   |PA   |RF   |AP   |PL   |VE   |UV   |PH   |GL      |LV
                |     |     |     |     |     |     |     |     |     |     |     |        |
        NA      | m   |     |     | n   |     |     |     |     |ɲ    |ŋ    |     |        |
        PL      |pb   |     |     |td   |     |     |     |     |kg   |     |     |ʔ       |
        SAF     |     |     |     |tsdz |tʃdʒ |ʈʂɖʐ |tɕdʑ |     |     |     |     |        |
        NAF     |     |     |tθdð |     |     |     |     |     |     |     |     |        |
        SFR     |     |fv   |θð   |sz   |ʃʒ   |ʂʐ   |ɕʑ   |     |x    |     |     |h       |
        NFR     |     |     |     |     |     |     |     |     |     |     |     |        |
        AP      |     | ʋ   |     | ɹ   |     |     |     |     | j   |     |     |        |ʍw
        TF      |     |     |     |     |     |     |     |     |     |     |     |        |
        TR      | B   |     |     | r   |     |     |     |     |     | R   |     |        |
        LF      |     |     |     |     |     |     |     |     |     |     |     |        |    
        LA      |     |     |     | l   |     |     |     |ʎ    |     |     |     |        |                                                   
        */

        public static readonly List<Consonant> IPAConsonants = new List<Consonant>
        {
            #region consonants
            #region nasals
            new Consonant("m̥", Consonant.Place.Bilabial, Consonant.Manner.Nasal, Consonant.Phonation.Unvoiced, 5),
            new Consonant("m", Consonant.Place.Bilabial, Consonant.Manner.Nasal, Consonant.Phonation.Voiced, 5),
            new Consonant("ɱ", Consonant.Place.Labiodental, Consonant.Manner.Nasal, Consonant.Phonation.Voiced, 5),
            new Consonant("n̼", Consonant.Place.Linguolabial, Consonant.Manner.Nasal, Consonant.Phonation.Voiced, 5),
            new Consonant("n̥", Consonant.Place.Alveolar, Consonant.Manner.Nasal, Consonant.Phonation.Unvoiced, 5),
            new Consonant("n", Consonant.Place.Alveolar, Consonant.Manner.Nasal, Consonant.Phonation.Voiced, 5),
            new Consonant("ɲ̊", Consonant.Place.Palatal, Consonant.Manner.Nasal, Consonant.Phonation.Unvoiced, 5),
            new Consonant("ɲ", Consonant.Place.Palatal, Consonant.Manner.Nasal, Consonant.Phonation.Voiced, 5),
            new Consonant("ŋ̊", Consonant.Place.Velar, Consonant.Manner.Nasal, Consonant.Phonation.Voiced, 5),
            new Consonant("ŋ", Consonant.Place.Velar, Consonant.Manner.Nasal, Consonant.Phonation.Voiced, 5),
            new Consonant("ɴ", Consonant.Place.Uvular, Consonant.Manner.Nasal, Consonant.Phonation.Voiced, 5),

            #endregion
            #region stops
            new Consonant("p", Consonant.Place.Bilabial, Consonant.Manner.Stop, Consonant.Phonation.Unvoiced, 9),
            new Consonant("b", Consonant.Place.Bilabial, Consonant.Manner.Stop, Consonant.Phonation.Voiced, 8),
            new Consonant("p̪", Consonant.Place.Labiodental, Consonant.Manner.Stop, Consonant.Phonation.Unvoiced, 9),
            new Consonant("b̪", Consonant.Place.Labiodental, Consonant.Manner.Stop, Consonant.Phonation.Voiced, 8),
            new Consonant("t̼", Consonant.Place.Linguolabial, Consonant.Manner.Stop, Consonant.Phonation.Unvoiced, 9),
            new Consonant("d̼", Consonant.Place.Linguolabial, Consonant.Manner.Stop, Consonant.Phonation.Voiced, 8),
            new Consonant("t", Consonant.Place.Alveolar, Consonant.Manner.Stop, Consonant.Phonation.Unvoiced, 9),
            new Consonant("d", Consonant.Place.Alveolar, Consonant.Manner.Stop, Consonant.Phonation.Voiced, 8),
            new Consonant("ʈ", Consonant.Place.Retroflex, Consonant.Manner.Stop, Consonant.Phonation.Unvoiced, 9),
            new Consonant("ɖ", Consonant.Place.Retroflex, Consonant.Manner.Stop, Consonant.Phonation.Voiced, 8),
            new Consonant("c", Consonant.Place.Palatal, Consonant.Manner.Stop, Consonant.Phonation.Unvoiced, 9),
            new Consonant("ɟ", Consonant.Place.Palatal, Consonant.Manner.Stop, Consonant.Phonation.Voiced, 8),
            new Consonant("k", Consonant.Place.Velar, Consonant.Manner.Stop, Consonant.Phonation.Unvoiced, 9),
            new Consonant("g", Consonant.Place.Velar, Consonant.Manner.Stop, Consonant.Phonation.Voiced, 8),
            new Consonant("q", Consonant.Place.Uvular, Consonant.Manner.Stop, Consonant.Phonation.Unvoiced, 9),
            new Consonant("ɢ", Consonant.Place.Uvular, Consonant.Manner.Stop, Consonant.Phonation.Voiced, 8),
            new Consonant("ʡ", Consonant.Place.Pharyngeal, Consonant.Manner.Stop, Consonant.Phonation.Unvoiced, 9),
            new Consonant("ʔ", Consonant.Place.Glottal, Consonant.Manner.Stop, Consonant.Phonation.Unvoiced, 9),

            #endregion
            #region sibilantaffricates
            new Consonant("t͡s", Consonant.Place.Alveolar, Consonant.Manner.SibilantAffricate, Consonant.Phonation.Unvoiced, 7),
            new Consonant("d͡z", Consonant.Place.Alveolar, Consonant.Manner.SibilantAffricate, Consonant.Phonation.Voiced, 6),
            new Consonant("t͡ʃ", Consonant.Place.Palatoalveolar, Consonant.Manner.SibilantAffricate, Consonant.Phonation.Unvoiced, 7),
            new Consonant("d͡ʒ", Consonant.Place.Palatoalveolar, Consonant.Manner.SibilantAffricate, Consonant.Phonation.Voiced, 6),
            new Consonant("ʈ͡ʂ", Consonant.Place.Retroflex, Consonant.Manner.SibilantAffricate, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ɖ͡ʐ", Consonant.Place.Retroflex, Consonant.Manner.SibilantAffricate, Consonant.Phonation.Voiced, 6),
            new Consonant("t͡ɕ", Consonant.Place.Alveolopalatal, Consonant.Manner.SibilantAffricate, Consonant.Phonation.Unvoiced, 7),
            new Consonant("d͡ʑ", Consonant.Place.Alveolopalatal, Consonant.Manner.SibilantAffricate, Consonant.Phonation.Voiced, 6),

            #endregion
            #region nonsibilantaffricates
            new Consonant("p͡ɸ", Consonant.Place.Bilabial, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Unvoiced, 7),
            new Consonant("b͡β", Consonant.Place.Bilabial, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Voiced, 6),
            new Consonant("p̪͡f", Consonant.Place.Labiodental, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Unvoiced, 7),
            new Consonant("b̪͡v", Consonant.Place.Labiodental, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Voiced, 6),
            new Consonant("t͡θ", Consonant.Place.Dental, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Unvoiced, 7),
            new Consonant("d͡ð", Consonant.Place.Dental, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Voiced, 6),
            new Consonant("t͡θ̠", Consonant.Place.Alveolar, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Unvoiced, 7),
            new Consonant("d͡ð̠", Consonant.Place.Alveolar, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Voiced, 6),
            new Consonant("t̠͡ɹ̠̊˔", Consonant.Place.Palatoalveolar, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Unvoiced, 7),
            new Consonant("d̠͡ɹ̠˔", Consonant.Place.Palatoalveolar, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Voiced, 6),
            new Consonant("c͡ç", Consonant.Place.Palatal, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ɟ͡ʝ", Consonant.Place.Palatal, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Voiced, 6),
            new Consonant("k͡x", Consonant.Place.Velar, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ɡ͡ɣ", Consonant.Place.Velar, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Voiced, 6),
            new Consonant("q͡χ", Consonant.Place.Uvular, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ʔ͡h", Consonant.Place.Glottal, Consonant.Manner.NonsibilantAffricate, Consonant.Phonation.Unvoiced, 7),

            #endregion
            #region sibilantfricatives
            new Consonant("s", Consonant.Place.Alveolar, Consonant.Manner.SibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("z", Consonant.Place.Alveolar, Consonant.Manner.SibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("ʃ", Consonant.Place.Palatal, Consonant.Manner.SibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ʒ", Consonant.Place.Palatal, Consonant.Manner.SibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("ʂ", Consonant.Place.Retroflex, Consonant.Manner.SibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ʐ", Consonant.Place.Retroflex, Consonant.Manner.SibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("ɕ", Consonant.Place.Alveolopalatal, Consonant.Manner.SibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ʑ", Consonant.Place.Alveolopalatal, Consonant.Manner.SibilantFricative, Consonant.Phonation.Voiced, 6),

            #endregion
            #region nonsibilantfricatives
            new Consonant("ɸ", Consonant.Place.Bilabial, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("β", Consonant.Place.Bilabial, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("f", Consonant.Place.Labiodental, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("v", Consonant.Place.Labiodental, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("θ̼", Consonant.Place.Linguolabial, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ð̼", Consonant.Place.Linguolabial, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("θ", Consonant.Place.Dental, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ð", Consonant.Place.Dental, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("θ̱", Consonant.Place.Alveolar, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ð̠", Consonant.Place.Alveolar, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("ɹ̠̊˔", Consonant.Place.Palatoalveolar, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ɹ̠˔", Consonant.Place.Palatoalveolar, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("ç", Consonant.Place.Palatal, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ʝ", Consonant.Place.Palatal, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("x", Consonant.Place.Velar, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ɣ", Consonant.Place.Velar, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("χ", Consonant.Place.Uvular, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ʁ", Consonant.Place.Uvular, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("ħ", Consonant.Place.Pharyngeal, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ʕ", Consonant.Place.Pharyngeal, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Voiced, 6),
            new Consonant("h", Consonant.Place.Glottal, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Unvoiced, 7),
            new Consonant("ɦ", Consonant.Place.Glottal, Consonant.Manner.NonsibilantFricative, Consonant.Phonation.Voiced, 6),

            #endregion
            #region approximants
            new Consonant("ʋ̥", Consonant.Place.Labiodental, Consonant.Manner.Approximant, Consonant.Phonation.Unvoiced, 6),
            new Consonant("ʋ", Consonant.Place.Labiodental, Consonant.Manner.Approximant, Consonant.Phonation.Voiced, 6),
            new Consonant("ɹ̥", Consonant.Place.Alveolar, Consonant.Manner.Approximant, Consonant.Phonation.Unvoiced, 3),
            new Consonant("ɹ", Consonant.Place.Alveolar, Consonant.Manner.Approximant, Consonant.Phonation.Voiced, 3),
            new Consonant("ɻ̊", Consonant.Place.Retroflex, Consonant.Manner.Approximant, Consonant.Phonation.Unvoiced, 3),
            new Consonant("ɻ", Consonant.Place.Retroflex, Consonant.Manner.Approximant, Consonant.Phonation.Voiced, 3),
            new Consonant("j̊", Consonant.Place.Palatal, Consonant.Manner.Approximant, Consonant.Phonation.Unvoiced, 2),
            new Consonant("j", Consonant.Place.Palatal, Consonant.Manner.Approximant, Consonant.Phonation.Voiced, 2),
            new Consonant("ɰ̊", Consonant.Place.Velar, Consonant.Manner.Approximant, Consonant.Phonation.Unvoiced, 2),
            new Consonant("ɰ", Consonant.Place.Velar, Consonant.Manner.Approximant, Consonant.Phonation.Voiced, 2),
            new Consonant("ʍ", Consonant.Place.Labializedvelar, Consonant.Manner.Approximant, Consonant.Phonation.Unvoiced, 2),
            new Consonant("w", Consonant.Place.Labializedvelar, Consonant.Manner.Approximant, Consonant.Phonation.Voiced, 2),

            #endregion
            #region flap or tap
            new Consonant("ⱱ", Consonant.Place.Labiodental, Consonant.Manner.FlapOrTap, Consonant.Phonation.Voiced, 3),
            new Consonant("ɾ̥", Consonant.Place.Alveolar, Consonant.Manner.FlapOrTap, Consonant.Phonation.Unvoiced, 3),
            new Consonant("ɾ", Consonant.Place.Alveolar, Consonant.Manner.FlapOrTap, Consonant.Phonation.Voiced, 3),
            new Consonant("ɽ̊", Consonant.Place.Retroflex, Consonant.Manner.FlapOrTap, Consonant.Phonation.Unvoiced, 3),
            new Consonant("ɽ", Consonant.Place.Retroflex, Consonant.Manner.FlapOrTap, Consonant.Phonation.Voiced, 3),

            #endregion
            #region trills
            new Consonant("B", Consonant.Place.Bilabial, Consonant.Manner.Trill, Consonant.Phonation.Voiced, 3),
            new Consonant("ʙ̪", Consonant.Place.Labiodental, Consonant.Manner.Trill, Consonant.Phonation.Voiced, 3),
            new Consonant("r̥", Consonant.Place.Alveolar, Consonant.Manner.Trill, Consonant.Phonation.Unvoiced, 3),
            new Consonant("r", Consonant.Place.Alveolar, Consonant.Manner.Trill, Consonant.Phonation.Voiced, 3),
            new Consonant("ʀ̥", Consonant.Place.Uvular, Consonant.Manner.Trill, Consonant.Phonation.Unvoiced, 3),
            new Consonant("R", Consonant.Place.Uvular, Consonant.Manner.Trill, Consonant.Phonation.Voiced, 3),
            new Consonant("ʜ", Consonant.Place.Pharyngeal, Consonant.Manner.Trill, Consonant.Phonation.Unvoiced, 3),
            new Consonant("ʢ", Consonant.Place.Pharyngeal, Consonant.Manner.Trill, Consonant.Phonation.Voiced, 3),

            #endregion
            #region lateral affricate
            new Consonant("t͡ɬ", Consonant.Place.Alveolar, Consonant.Manner.LateralAffricate, Consonant.Phonation.Unvoiced, 4),
            new Consonant("d͡ɮ", Consonant.Place.Alveolar, Consonant.Manner.LateralAffricate, Consonant.Phonation.Voiced, 4),
            new Consonant("c͡ʎ̥˔", Consonant.Place.Palatal, Consonant.Manner.LateralAffricate, Consonant.Phonation.Unvoiced, 4),
            new Consonant("k͡ʟ̝̊", Consonant.Place.Velar, Consonant.Manner.LateralAffricate, Consonant.Phonation.Unvoiced, 4),
            new Consonant("ɡ͡ʟ̝", Consonant.Place.Velar, Consonant.Manner.LateralAffricate, Consonant.Phonation.Voiced, 4),

            #endregion
            #region lateral fricative
            new Consonant("ɬ", Consonant.Place.Alveolar, Consonant.Manner.LateralFricative, Consonant.Phonation.Unvoiced, 4),
            new Consonant("ɮ", Consonant.Place.Alveolar, Consonant.Manner.LateralFricative, Consonant.Phonation.Voiced, 4),
            new Consonant("ʎ̥˔", Consonant.Place.Palatal, Consonant.Manner.LateralFricative, Consonant.Phonation.Unvoiced, 4),
            new Consonant("ʎ̝", Consonant.Place.Palatal, Consonant.Manner.LateralFricative, Consonant.Phonation.Voiced, 4),
            new Consonant("ʟ̝̊", Consonant.Place.Velar, Consonant.Manner.LateralFricative, Consonant.Phonation.Unvoiced, 4),
            new Consonant("ʟ̝", Consonant.Place.Velar, Consonant.Manner.LateralFricative, Consonant.Phonation.Voiced, 4),

            #endregion

            #region lateral approximants
            new Consonant("l̥", Consonant.Place.Alveolar, Consonant.Manner.LateralApproximant, Consonant.Phonation.Unvoiced, 4),
            new Consonant("l", Consonant.Place.Alveolar, Consonant.Manner.LateralApproximant, Consonant.Phonation.Voiced, 4),
            new Consonant("ɭ̊", Consonant.Place.Retroflex, Consonant.Manner.LateralApproximant, Consonant.Phonation.Unvoiced, 4),
            new Consonant("ɭ", Consonant.Place.Retroflex, Consonant.Manner.LateralApproximant, Consonant.Phonation.Voiced, 4),
            new Consonant("ʎ̥", Consonant.Place.Palatal, Consonant.Manner.LateralApproximant, Consonant.Phonation.Unvoiced, 4),
            new Consonant("ʎ", Consonant.Place.Palatal, Consonant.Manner.LateralApproximant, Consonant.Phonation.Voiced, 4),
            new Consonant("ʟ̥", Consonant.Place.Velar, Consonant.Manner.LateralApproximant, Consonant.Phonation.Unvoiced, 4),
            new Consonant("ʟ", Consonant.Place.Velar, Consonant.Manner.LateralApproximant, Consonant.Phonation.Voiced, 4),
            new Consonant("ʟ̠", Consonant.Place.Uvular, Consonant.Manner.LateralApproximant, Consonant.Phonation.Voiced, 4),

            #endregion
            #region lateral flap
            new Consonant("ɺ", Consonant.Place.Alveolar, Consonant.Manner.LateralFlap, Consonant.Phonation.Voiced, 4),
            new Consonant("ɭ̆", Consonant.Place.Retroflex, Consonant.Manner.LateralFlap, Consonant.Phonation.Voiced, 4),
            new Consonant("ʎ̮", Consonant.Place.Palatal, Consonant.Manner.LateralFlap, Consonant.Phonation.Voiced, 4),
            new Consonant("ʟ̆", Consonant.Place.Velar, Consonant.Manner.LateralFlap, Consonant.Phonation.Voiced, 4),

            #endregion
            #endregion
        };

        public static ConsonantCollection AllConsonants => new ConsonantCollection();

        public static ConsonantCollection DefaultConsonants => EnglishConsonants;

        public static ConsonantCollection EnglishConsonants
        {
            get
            {
                var list = new List<string>
                {
                    "m", "n", "ŋ",
                    "p", "t", "t͡ʃ", "k",
                    "b", "d", "d͡ʒ", "g",
                    "s", "ʃ",
                    "z", "ʒ",
                    "f", "θ",
                    "v", "ð", "h",
                    "l", "ɹ", "j", "w"
                };
                var c = new ConsonantCollection(new List<Predicate<Consonant>>
                {
                    consonant => list.Contains(consonant.Symbol)
                });
                return c;
            }
        }

        public ConsonantCollection() : base(DefaultConsonants)
        {
        }

        public ConsonantCollection(List<Predicate<Consonant>> add)
        {
            add.ForEach(pred => { AddRange(IPAConsonants.Where(c => pred(c))); });
        }

        public ConsonantCollection(List<Consonant> consonants)
        {
            consonants.ForEach(x => this.Add(x));
        }

        public Consonant GetRandom()
        {
            return this[Globals.Random.Next(Count)];
        }

        public Consonant GetRandomInSonorityRange(int sonorityMin, int sonorityMax)
        {
            var sublist = this.Where(x => x.Sonority <= sonorityMax && x.Sonority >= sonorityMin).ToList();
            if (sublist.Count == 0) return null;
            return sublist.ElementAt(Globals.Random.Next(sublist.Count));
        }

        public static ConsonantCollection Generate()
        {
            var places = new List<Consonant.Place>();
            var allPlaces = new Queue<Consonant.Place>(IPAConsonants.Select(x => x.PlaceOfArticulation).Distinct().OrderBy(x => Globals.Random.Next()).ToList());

            var manners = new List<Consonant.Manner>();
            var allManners = new Queue<Consonant.Manner>(IPAConsonants.Select(x => x.MannerOfArticulation).Distinct().OrderBy(x => Globals.Random.Next()).ToList());

            // numbers from WALS http://wals.info/chapter/1
            var probabilities = new Dictionary<Tuple<int, int>, float>
            {
                {new Tuple<int, int>(6, 14), 89f},
                {new Tuple<int, int>(15, 18), 122f},
                {new Tuple<int, int>(19, 25), 201f},
                {new Tuple<int, int>(26, 33), 94f},
                {new Tuple<int, int>(34, IPAConsonants.Count), 57f}
            };
            var consonantsCount = Globals.WeightedRandom(probabilities);

            // numbers again from WALS http://wals.info/chapter/4
            var voicingProbs = new Dictionary<Tuple<bool, bool>, float>
            {
                {new Tuple<bool, bool>(false, false), 182f},
                {new Tuple<bool, bool>(false, true), 189f},
                {new Tuple<bool, bool>(true, false), 38f},
                {new Tuple<bool, bool>(true, true), 158f},
            };
            var voicingRules = Globals.WeightedRandom(voicingProbs);

            var fricVoiceContrast = voicingRules.Item1;
            var stopVoiceContrast = voicingRules.Item2;

            var fricDefaultVoicing = Globals.Random.Next(100) > 50 ? Consonant.Phonation.Unvoiced : Consonant.Phonation.Voiced;
            var stopDefaultVoicing = Globals.Random.Next(100) > 50 ? Consonant.Phonation.Unvoiced : Consonant.Phonation.Voiced;

            // we have a list of extra additions in case we run out of places+manners
            var listOfExtraConsonants = new List<Consonant>();

            // check if the consonant is included in our places & manners
            // if so, check if we want both voiced&unvoiced, or just the default
            Func<Consonant, bool> test = x =>
            {
                var e = places.Contains(x.PlaceOfArticulation)
                        && manners.Contains(x.MannerOfArticulation);

                var b = false;
                if (x.MannerOfArticulation == Consonant.Manner.Stop)
                {
                    b = stopVoiceContrast || x.Voicing == stopDefaultVoicing;
                }
                else if (x.MannerOfArticulation == Consonant.Manner.SibilantFricative || x.MannerOfArticulation == Consonant.Manner.NonsibilantFricative)
                {
                    b = fricVoiceContrast || x.Voicing == fricDefaultVoicing;
                }

                return (b && e) || listOfExtraConsonants.Contains(x);
            };

            var collection = new List<Consonant>();
            while (collection.Count < consonantsCount.Item1)
            {
                if ((Globals.Random.Next(100) < 50 || allManners.Count == 0) && allPlaces.Count > 0)
                {
                    var p = allPlaces.Dequeue();

                    places.Add(p);
                }
                else if (allManners.Count > 0)
                {
                    var m = allManners.Dequeue();

                    manners.Add(m);
                }
                else
                {
                    //places and manners lists are empty, add some random consonants
                    var c = IPAConsonants[Globals.Random.Next(IPAConsonants.Count)];
                    while (collection.Contains(c)) c = IPAConsonants[Globals.Random.Next(IPAConsonants.Count)];
                    listOfExtraConsonants.Add(c);

                    //throw new Exception("Places and Manners lists are both empty.");
                }

                collection = IPAConsonants.Where(test).ToList();
            }

            return new ConsonantCollection(new List<Predicate<Consonant>> {x => test(x)});
        }

        public static ConsonantCollection GenerateSubset(ConsonantCollection collection)
        {
            var num = Math.Max(2, Globals.Random.Next(collection.Count));
            var stack = new Stack<Consonant>(collection.OrderBy(x => Globals.Random.Next()));
            var list = new List<Consonant>();
            for (var i = 0; i < num; i++)
            {
                list.Add(stack.Pop());
            }
            return new ConsonantCollection(new List<Predicate<Consonant>> {x => list.Contains(x)});
        }

        public JToken ToJSON()
        {
            return new JArray(this.Select(x => x.Symbol));
        }

        public static ConsonantCollection FromJSON(JToken jToken)
        {
            var a = (jToken as JArray).ToList().Select(x => x.Value<string>());
            var consonants = IPAConsonants.Where(x => a.Contains(x.Symbol)).ToList();
            ConsonantCollection c = new ConsonantCollection(consonants);
            return c;
        }
    }
}