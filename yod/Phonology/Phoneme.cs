namespace yod.Phonology
{
    public abstract class Phoneme
    {
        public string Symbol;
        public PhonemeType Type;
        public int Sonority;

        public override string ToString()
        {
            return Symbol;
        }

        public enum PhonemeType
        {
            Consonant,
            Vowel
        }
    }
}
