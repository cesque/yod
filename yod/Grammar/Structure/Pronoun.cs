using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Pronoun : TerminalPhrase
    {
        public Pronoun(string english, List<string> tags) : base(english, PartOfSpeech.PRONOUN, tags) { }
        public Pronoun(string english, string tags) : base(english, PartOfSpeech.PRONOUN, tags) { }
    }
}
