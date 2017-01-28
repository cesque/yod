using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Determiner : TerminalPhrase
    {
        public Determiner(string english, List<string> tags) : base(english, PartOfSpeech.DETERMINER, tags) { }
        public Determiner(string english, string tags) : base(english, PartOfSpeech.DETERMINER, tags) { }
    }
}
