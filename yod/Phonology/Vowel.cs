namespace yod.Phonology
{
    /// <summary>
    /// Represents a single vowel phoneme. The <c>long</c> member in this case represents whether this is a long vowel or not.
    /// </summary>
    public class Vowel : Phoneme
    {
        /// <summary>
        /// Forward-backward position of the tongue.
        /// </summary>
        public enum Backness
        {
            Front,
            NearFront,
            Central,
            NearBack,
            Back
        }

        /// <summary>
        /// Vertical position of the tongue.
        /// </summary>
        public enum Height
        {
            Close,
            NearClose,
            CloseMid,
            Mid,
            OpenMid,
            NearOpen,
            Open
        }

        /// <summary>
        /// Formation of the lips.
        /// </summary>
        public enum Rounding
        {
            Rounded,
            Unrounded
        }

        /// <summary>
        /// The forward-backward position of the tongue when pronouncing this vowel.
        /// </summary>
        public Backness Frontedness;
        /// <summary>
        /// The vertical position of the tongue when pronouncing this vowel.
        /// </summary>
        public Height Openness;
        /// <summary>
        /// The formation of the lips when pronouncing this vowel.
        /// </summary>
        public Rounding Roundedness;

        /// <summary>
        /// Creates a copy of a vowel.
        /// </summary>
        /// <param name="vowel">The vowel to create another instance of.</param>
        public Vowel(Vowel vowel)
        {
            Frontedness = vowel.Frontedness;
            Openness = vowel.Openness;
            Roundedness = vowel.Roundedness;
            Symbol = vowel.Symbol;
            Type = vowel.Type;
            Sonority = vowel.Sonority;
        }

        /// <summary>
        /// Create a new vowel.
        /// </summary>
        /// <remarks>This is primarily used within the library, but could also be useful if you wanted to add new nonsyllabic consonants to a phonology, such as in some Slavic languages.</remarks>
        /// <param name="symbol"></param>
        /// <param name="openness"></param>
        /// <param name="frontness"></param>
        /// <param name="roundedness"></param>
        /// <param name="sonority"></param>
        public Vowel(string symbol, Height openness, Backness frontness, Rounding roundedness, int sonority)
        {
            Symbol = symbol;
            Frontedness = frontness;
            Openness = openness;
            Roundedness = roundedness;
            Type = PhonemeType.Vowel;
            Sonority = sonority;
        }
    }
}
