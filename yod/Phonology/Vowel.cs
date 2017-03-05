namespace yod.Phonology
{
    public class Vowel : Phoneme
    {
        public enum Backness
        {
            Front,
            NearFront,
            Central,
            NearBack,
            Back
        }

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

        public enum Rounding
        {
            Rounded,
            Unrounded
        }

        public Backness Frontedness;
        public Height Openness;
        public Rounding Roundedness;

        public Vowel(Vowel vowel)
        {
            Frontedness = vowel.Frontedness;
            Openness = vowel.Openness;
            Roundedness = vowel.Roundedness;
            Symbol = vowel.Symbol;
            Type = vowel.Type;
            Sonority = vowel.Sonority;
        }

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
