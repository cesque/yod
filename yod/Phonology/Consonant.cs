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
            Sibilantaffricate,
            Nonsibilantaffricate,
            Sibilantfricative,
            Nonsibilantfricative,
            Approximant,
            Flaportap,
            Trill,
            Lateralaffricate,
            Lateralfricative,
            Lateralapproximant,
            Lateralflap
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
