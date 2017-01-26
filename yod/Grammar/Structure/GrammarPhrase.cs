using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public abstract class GrammarPhrase
    {
        public bool IsTerminal;
        public string Tag;

        public abstract List<InputWord> Flatten();
    }
}
