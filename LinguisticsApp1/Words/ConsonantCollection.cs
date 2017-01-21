using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod
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

        private static readonly List<Consonant> IPAConsonants = new List<Consonant>
        {
            #region consonants
            #region nasals
            new Consonant("m", Consonant.Place.BILABIAL, Consonant.Manner.NASAL, Consonant.Phonation.VOICED, 5),
            new Consonant("n", Consonant.Place.ALVEOLAR, Consonant.Manner.NASAL, Consonant.Phonation.VOICED, 5),
            new Consonant("ɲ", Consonant.Place.PALATAL, Consonant.Manner.NASAL, Consonant.Phonation.VOICED, 5),
            new Consonant("ŋ", Consonant.Place.VELAR, Consonant.Manner.NASAL, Consonant.Phonation.VOICED, 5),
            #endregion
            #region stops
            new Consonant("p", Consonant.Place.BILABIAL, Consonant.Manner.STOP, Consonant.Phonation.UNVOICED, 9),
            new Consonant("b", Consonant.Place.BILABIAL, Consonant.Manner.STOP, Consonant.Phonation.VOICED, 8),
            new Consonant("t", Consonant.Place.ALVEOLAR, Consonant.Manner.STOP, Consonant.Phonation.UNVOICED, 9),
            new Consonant("d", Consonant.Place.ALVEOLAR, Consonant.Manner.STOP, Consonant.Phonation.VOICED, 8),
            new Consonant("k", Consonant.Place.VELAR, Consonant.Manner.STOP, Consonant.Phonation.UNVOICED, 9),
            new Consonant("g", Consonant.Place.VELAR, Consonant.Manner.STOP, Consonant.Phonation.VOICED, 8),
            new Consonant("ʔ", Consonant.Place.GLOTTAL, Consonant.Manner.STOP, Consonant.Phonation.UNVOICED, 9),
            #endregion
            #region sibilantaffricates
            new Consonant("t͡s", Consonant.Place.ALVEOLAR, Consonant.Manner.SIBILANTAFFRICATE, Consonant.Phonation.UNVOICED, 7),
            new Consonant("d͡z", Consonant.Place.ALVEOLAR, Consonant.Manner.SIBILANTAFFRICATE, Consonant.Phonation.VOICED, 6),
            new Consonant("t͡ʃ", Consonant.Place.PALATOALVEOLAR, Consonant.Manner.SIBILANTAFFRICATE, Consonant.Phonation.UNVOICED, 7),
            new Consonant("d͡ʒ", Consonant.Place.PALATOALVEOLAR, Consonant.Manner.SIBILANTAFFRICATE, Consonant.Phonation.VOICED, 6),
            new Consonant("ʈ͡ʂ", Consonant.Place.RETROFLEX, Consonant.Manner.SIBILANTAFFRICATE, Consonant.Phonation.UNVOICED, 7),
            new Consonant("ɖ͡ʐ", Consonant.Place.RETROFLEX, Consonant.Manner.SIBILANTAFFRICATE, Consonant.Phonation.VOICED, 6),
            new Consonant("t͡ɕ", Consonant.Place.ALVEOLOPALATAL, Consonant.Manner.SIBILANTAFFRICATE, Consonant.Phonation.UNVOICED, 7),
            new Consonant("d͡ʑ", Consonant.Place.ALVEOLOPALATAL, Consonant.Manner.SIBILANTAFFRICATE, Consonant.Phonation.VOICED, 6),
            
            #endregion
            #region nonsibilantaffricates
            new Consonant("t͡θ", Consonant.Place.DENTAL, Consonant.Manner.NONSIBILANTAFFRICATE, Consonant.Phonation.UNVOICED, 7),
            new Consonant("d͡ð", Consonant.Place.DENTAL, Consonant.Manner.NONSIBILANTAFFRICATE, Consonant.Phonation.VOICED, 6),
            #endregion
            #region sibilantfricatives
            new Consonant("s", Consonant.Place.ALVEOLAR, Consonant.Manner.SIBILANTFRICATIVE, Consonant.Phonation.UNVOICED, 7),
            new Consonant("z", Consonant.Place.ALVEOLAR, Consonant.Manner.SIBILANTFRICATIVE, Consonant.Phonation.VOICED, 6),
            new Consonant("ʃ", Consonant.Place.PALATAL, Consonant.Manner.SIBILANTFRICATIVE, Consonant.Phonation.UNVOICED, 7),
            new Consonant("ʒ", Consonant.Place.PALATAL, Consonant.Manner.SIBILANTFRICATIVE, Consonant.Phonation.VOICED, 6),
            new Consonant("ʂ", Consonant.Place.RETROFLEX, Consonant.Manner.SIBILANTFRICATIVE, Consonant.Phonation.UNVOICED, 7),
            new Consonant("ʐ", Consonant.Place.RETROFLEX, Consonant.Manner.SIBILANTFRICATIVE, Consonant.Phonation.VOICED, 6),
            new Consonant("ɕ", Consonant.Place.ALVEOLOPALATAL, Consonant.Manner.SIBILANTFRICATIVE, Consonant.Phonation.UNVOICED, 7),
            new Consonant("ʑ", Consonant.Place.ALVEOLOPALATAL, Consonant.Manner.SIBILANTFRICATIVE, Consonant.Phonation.VOICED, 6),         
            #endregion
            #region nonsibilantfricatives
            new Consonant("f", Consonant.Place.LABIODENTAL, Consonant.Manner.NONSIBILANTFRICATIVE, Consonant.Phonation.UNVOICED, 7),
            new Consonant("v", Consonant.Place.LABIODENTAL, Consonant.Manner.NONSIBILANTFRICATIVE, Consonant.Phonation.VOICED, 6),
            new Consonant("θ", Consonant.Place.DENTAL, Consonant.Manner.NONSIBILANTFRICATIVE, Consonant.Phonation.UNVOICED, 7),
            new Consonant("ð", Consonant.Place.DENTAL, Consonant.Manner.NONSIBILANTFRICATIVE, Consonant.Phonation.VOICED, 6),
            new Consonant("x", Consonant.Place.VELAR, Consonant.Manner.NONSIBILANTFRICATIVE, Consonant.Phonation.UNVOICED, 7),
            new Consonant("h", Consonant.Place.GLOTTAL, Consonant.Manner.NONSIBILANTFRICATIVE, Consonant.Phonation.UNVOICED, 7),
            #endregion
            #region approximants
            new Consonant("ʋ", Consonant.Place.LABIODENTAL, Consonant.Manner.APPROXIMANT, Consonant.Phonation.VOICED, 6),
            new Consonant("ɹ", Consonant.Place.ALVEOLAR, Consonant.Manner.APPROXIMANT, Consonant.Phonation.VOICED, 3),
            new Consonant("j", Consonant.Place.PALATAL, Consonant.Manner.APPROXIMANT, Consonant.Phonation.VOICED, 2),
            new Consonant("ʍ", Consonant.Place.LABIALIZEDVELAR, Consonant.Manner.APPROXIMANT, Consonant.Phonation.UNVOICED, 2),
            new Consonant("w", Consonant.Place.LABIALIZEDVELAR, Consonant.Manner.APPROXIMANT, Consonant.Phonation.VOICED, 2),
            #endregion
            #region flap or tap
            #endregion
            #region trills
            new Consonant("B", Consonant.Place.BILABIAL, Consonant.Manner.TRILL, Consonant.Phonation.VOICED, 3),
            new Consonant("r", Consonant.Place.ALVEOLAR, Consonant.Manner.TRILL, Consonant.Phonation.VOICED, 3),
            new Consonant("R", Consonant.Place.UVULAR, Consonant.Manner.TRILL, Consonant.Phonation.VOICED, 3),
            #endregion
            #region lateral affricate
            #endregion
            #region lateral fricative
            #endregion

            #region lateral approximants
            new Consonant("l", Consonant.Place.ALVEOLAR, Consonant.Manner.LATERALAPPROXIMANT, Consonant.Phonation.VOICED, 4),
            new Consonant("ʎ", Consonant.Place.PALATAL, Consonant.Manner.LATERALAPPROXIMANT, Consonant.Phonation.VOICED, 4),
            #endregion
            #region lateral flap
            #endregion
            #endregion
        };

        public static ConsonantCollection AllConsonants
        {
            get
            {
                return new ConsonantCollection();
            }
        }

        public static ConsonantCollection DefaultConsonants
        {
            get
            {
                var c = new ConsonantCollection(new List<Predicate<Consonant>>()
                {
                    consonant => (consonant.PlaceOfArticulation != Consonant.Place.DENTAL)
                    && (consonant.PlaceOfArticulation != Consonant.Place.RETROFLEX)
                    && (consonant.PlaceOfArticulation != Consonant.Place.ALVEOLOPALATAL)
                    && (consonant.MannerOfArticulation != Consonant.Manner.TRILL)
                    && (consonant.Symbol != "ʔ")
                    && (consonant.Symbol != "ʋ")
                });
                return c;
            }
        }

        public ConsonantCollection() : base(IPAConsonants)
        {
        }

        public ConsonantCollection(List<Predicate<Consonant>> add)
        {
            add.ForEach(pred =>
            {
                this.AddRange(IPAConsonants.Where(c => pred(c)));
            });
        }

        public Consonant GetRandom()
        {
            return this[Globals.Random.Next(this.Count)];
        }

        public Consonant GetRandomInSonorityRange(int sonorityMin, int sonorityMax)
        {
            var sublist = this.Where(x => x.Sonority <= sonorityMax && x.Sonority >= sonorityMin);
            return sublist.ElementAt(Globals.Random.Next(sublist.Count()));
        }
    }
}
