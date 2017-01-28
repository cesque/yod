using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Noun : TerminalPhrase
    {
        public Noun(string english, List<string> tags) : base(english, PartOfSpeech.NOUN, tags) { }
        public Noun(string english, string tags) : base(english, PartOfSpeech.NOUN, tags) { }
    }
}
