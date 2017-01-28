using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Verb : TerminalPhrase
    {
        public Verb(string english, List<string> tags) : base(english, PartOfSpeech.VERB, tags) { }
        public Verb(string english, string tags) : base(english, PartOfSpeech.VERB, tags) { }
    }
}
