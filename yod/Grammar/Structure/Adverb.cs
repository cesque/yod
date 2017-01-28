using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Adverb : TerminalPhrase
    {
        public Adverb(string english, List<string> tags) : base(english, PartOfSpeech.ADVERB, tags) { }
        public Adverb(string english, string tags) : base(english, PartOfSpeech.ADVERB, tags) { }
    }
}
