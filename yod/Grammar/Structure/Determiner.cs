using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Determiner : GrammarPhrase
    {
        public InputWord Word;

        public Determiner(string english, List<string> tags)
        {
            IsTerminal = true;
            Tag = "DETM";
            Word = new InputWord(english, PartOfSpeech.DETERMINER, tags);
        }

        public Determiner(string english, string tags) : this(english, tags.Split(',').ToList()) { }

        public override List<InputWord> Flatten()
        {
            return new List<InputWord>() { Word };
        }
    }
}
