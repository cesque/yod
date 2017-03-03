namespace yod.Phonology
{
    public class Vowel : Phoneme
    {
        public enum FrontToBack
        {
            Front,
            NearFront,
            Central,
            NearBack,
            Back
        }

        public enum OpenToClose
        {
            Close,
            NearClose,
            CloseMid,
            Mid,
            OpenMid,
            NearOpen,
            Open
        }

        public enum Rounded
        {
            Rounded,
            Unrounded
        }

        public FrontToBack Frontedness;
        public OpenToClose Openness;
        public Rounded Roundedness;

        public Vowel(Vowel vowel)
        {
            Frontedness = vowel.Frontedness;
            Openness = vowel.Openness;
            Roundedness = vowel.Roundedness;
            Symbol = vowel.Symbol;
            Type = vowel.Type;
            Sonority = vowel.Sonority;
        }

        public Vowel(string symbol, OpenToClose openness, FrontToBack frontness, Rounded roundedness, int sonority)
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
