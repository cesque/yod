using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Conjunction : TerminalPhrase
    {
        public Conjunction(string english, List<string> tags) : base(english, PartOfSpeech.CONJUNCTION, tags) { }
        public Conjunction(string english, string tags) : base(english, PartOfSpeech.CONJUNCTION, tags) { }
    }
}
