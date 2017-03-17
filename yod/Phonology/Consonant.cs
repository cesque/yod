namespace yod.Phonology
{
    /// <summary>
    /// Represents a single consonant phoneme. The <c>long</c> member in this case represents whether this is a geminate consonant or not.
    /// </summary>
    public class Consonant : Phoneme
    {
        /// <summary>
        /// Place of articulation.
        /// </summary>
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

        /// <summary>
        /// Manner of articulation.
        /// </summary>
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

        /// <summary>
        /// Phonation/voicing.
        /// </summary>
        public enum Phonation
        {
            Voiced,
            Unvoiced
        }

        /// <summary>
        /// The place of articulation of this consonant.
        /// </summary>
        public Place PlaceOfArticulation;
        /// <summary>
        /// The manner of articulation of this consonant.
        /// </summary>
        public Manner MannerOfArticulation;
        /// <summary>
        /// The voicing of this consonant.
        /// </summary>
        public Phonation Voicing;

        /// <summary>
        /// Create a copy of a consonant.
        /// </summary>
        /// <param name="consonant">The consonant to create another instance of.</param>
        public Consonant(Consonant consonant)
        {
            Type = PhonemeType.Consonant;
            PlaceOfArticulation = consonant.PlaceOfArticulation;
            MannerOfArticulation = consonant.MannerOfArticulation;
            Voicing = consonant.Voicing;
            Symbol = consonant.Symbol;
            Sonority = consonant.Sonority;
        }

        /// <summary>
        /// Create a new consonant.
        /// </summary>
        /// <remarks>This is primarily used within the library, but could also be useful if you wanted to add new syllabic vowels to a phonology
        /// (although these are rare other than glides and semivowels, which are already present).</remarks>
        /// <param name="symbol">The IPA symbol.</param>
        /// <param name="place">The place of articulation.</param>
        /// <param name="manner">The manner of articulation.</param>
        /// <param name="voiced">The voicing.</param>
        /// <param name="sonority">The sonority/amplitude.</param>
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