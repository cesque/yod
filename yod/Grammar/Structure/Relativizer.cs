using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Relativizer : TerminalPhrase
    {
        public Relativizer(string english, List<string> tags) : base(english, PartOfSpeech.RELATIVIZER, tags) { }
        public Relativizer(string english, string tags) : base(english, PartOfSpeech.RELATIVIZER, tags) { }
    }
}
