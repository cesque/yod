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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var phoneme = (Phoneme) obj;
            return phoneme.Symbol == Symbol;
        }
    }
}