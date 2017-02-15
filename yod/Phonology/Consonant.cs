namespace yod.Phonology
{
    public class Consonant : Phoneme
    {
        public enum Place
        {
            Bilabial,
            Labiodental,
            Linguolabial,
            Dental,
            Alveolar,
            Palatoalveolar,
            Retroflex,
            Alveolopalatal,
            Palatal,
            Velar,
            Uvular,
            Pharyngeal,
            Glottal,

            Labializedvelar
        }

        public enum Manner
        {
            Nasal,
            Stop,
            SibilantAffricate,
            NonsibilantAffricate,
            SibilantFricative,
            NonsibilantFricative,
            Approximant,
            FlapOrTap,
            Trill,
            LateralAffricate,
            LateralFricative,
            LateralApproximant,
            LateralFlap
        }

        public enum Phonation
        {
            Voiced,
            Unvoiced
        }

        public Place PlaceOfArticulation;
        public Manner MannerOfArticulation;
        public Phonation Voicing;

        public Consonant(Consonant consonant)
        {
            Type = PhonemeType.Consonant;
            PlaceOfArticulation = consonant.PlaceOfArticulation;
            MannerOfArticulation = consonant.MannerOfArticulation;
            Voicing = consonant.Voicing;
            Symbol = consonant.Symbol;
            Sonority = consonant.Sonority;
        }

        public Consonant(string symbol, Place place, Manner manner, Phonation voiced, int sonority)
        {
            Symbol = symbol;
            PlaceOfArticulation = place;
            MannerOfArticulation = manner;
            Voicing = voiced;
            Type = PhonemeType.Consonant;
            Sonority = sonority;
        }
    }
}
