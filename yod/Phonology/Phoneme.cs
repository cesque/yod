namespace yod.Phonology
{
    /// <summary>
    /// Represents a single phoneme - either a consonant or a vowel (check <c>Type</c> member to check whether this phoneme is a consonant or a vowel)
    /// </summary>
    public abstract class Phoneme
    {
        /// <summary>
        /// The IPA symbol for this phoneme.
        /// </summary>
        public string Symbol;
        /// <summary>
        /// The type of this phoneme (consonant or vowel).
        /// </summary>
        public PhonemeType Type;
        /// <summary>
        /// The sonority value (aka amplitude) of this phoneme.
        /// </summary>
        public int Sonority;

        /// <summary>
        /// Whether the phoneme is considered 'long' (a long vowel in the case of vowels, or a geminate consonant in the case of consonants).
        /// </summary>
        public bool Long;

        /// <inheritdoc />
        public override string ToString()
        {
            return Symbol + (Long ? "ː" : "");
        }

        /// <summary>
        /// Type of phoneme.
        /// </summary>
        public enum PhonemeType
        {
            Consonant,
            Vowel
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var phoneme = (Phoneme) obj;
            return phoneme.Symbol == Symbol;
        }
    }
}