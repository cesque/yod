namespace yod.Phonology
{
    public class Vowel : Phoneme
    {
        public enum FrontToBack
        {
            Front,
            Nearfront,
            Central,
            Nearback,
            Back
        }

        public enum OpenToClose
        {
            Close,
            Nearclose,
            Closemid,
            Mid,
            Openmid,
            Nearopen,
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

        public Vowel(string symbol, FrontToBack frontness, OpenToClose openness, Rounded roundedness, int sonority)
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
