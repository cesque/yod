using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Preposition : TerminalPhrase
    {
        public Preposition(string english, List<string> tags) : base(english, PartOfSpeech.PREPOSITION, tags) { }
        public Preposition(string english, string tags) : base(english, PartOfSpeech.PREPOSITION, tags) { }
    }
}
