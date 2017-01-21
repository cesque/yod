using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod
{
    public class Consonant : Phoneme
    {
        public enum Place
        {
            BILABIAL,
            LABIODENTAL,
            LINGUOLABIAL,
            DENTAL,
            ALVEOLAR,
            PALATOALVEOLAR,
            RETROFLEX,
            ALVEOLOPALATAL,
            PALATAL,
            VELAR,
            UVULAR,
            PHARYNGEAL,
            GLOTTAL,

            LABIALIZEDVELAR
        }

        public enum Manner
        {
            NASAL,
            STOP,
            SIBILANTAFFRICATE,
            NONSIBILANTAFFRICATE,
            SIBILANTFRICATIVE,
            NONSIBILANTFRICATIVE,
            APPROXIMANT,
            FLAPORTAP,
            TRILL,
            LATERALAFFRICATE,
            LATERALFRICATIVE,
            LATERALAPPROXIMANT,
            LATERALFLAP,
        }

        public enum Phonation
        {
            VOICED,
            UNVOICED
        }

        public Place PlaceOfArticulation;
        public Manner MannerOfArticulation;
        public Phonation Voicing;

        public Consonant(string symbol, Place place, Manner manner, Phonation voiced, int sonority)
        {
            Symbol = symbol;
            PlaceOfArticulation = place;
            MannerOfArticulation = manner;
            Voicing = voiced;
            Type = PhonemeType.CONSONANT;
            Sonority = sonority;
        }
    }
}
