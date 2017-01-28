using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Adjective : TerminalPhrase
    {
        public Adjective(string english, List<string> tags) : base(english, PartOfSpeech.ADJECTIVE, tags) { }
        public Adjective(string english, string tags) : base(english, PartOfSpeech.ADJECTIVE, tags) { }
    }
}
