using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yodWords
{
    class Vowel : Phoneme
    {
        public enum FrontToBack
        {
            FRONT,
            NEARFRONT,
            CENTRAL,
            NEARBACK,
            BACK
        }

        public enum OpenToClose
        {
            CLOSE,
            NEARCLOSE,
            CLOSEMID,
            MID,
            OPENMID,
            NEAROPEN,
            OPEN
        }

        public enum Rounded
        {
            ROUNDED,
            UNROUNDED
        }

        public FrontToBack Frontedness;
        public OpenToClose Openness;
        public Rounded Roundedness;

        public Vowel(string symbol, FrontToBack frontness, OpenToClose openness, Rounded roundedness, int sonority)
        {
            Symbol = symbol;
            Frontedness = frontness;
            Openness = openness;
            Roundedness = roundedness;
            Type = PhonemeType.VOWEL;
            Sonority = sonority;
        }
    }
}
