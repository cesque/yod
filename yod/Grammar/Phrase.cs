using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar
{
    public abstract class Phrase
    {
        public abstract void Fill(Lexicon lexicon);
        public abstract List<Word> Flatten();
    }
}
