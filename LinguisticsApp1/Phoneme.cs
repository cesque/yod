using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yodWords
{
    abstract class Phoneme
    {
        public string Symbol;
        public PhonemeType Type;
        public int Sonority;

        override public string ToString()
        {
            return Symbol;
        }

        public enum PhonemeType
        {
            CONSONANT,
            VOWEL
        }
    }
}
