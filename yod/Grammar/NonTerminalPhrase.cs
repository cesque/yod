using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar
{
    public abstract class NonTerminalPhrase : Phrase
    {
        public readonly string Rule;

        public NonTerminalPhrase(string rule)
        {
            Rule = rule;
        }
    }
}
