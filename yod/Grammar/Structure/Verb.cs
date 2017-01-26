using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Verb : GrammarPhrase
    {
        public InputWord Word;

        public Verb(string english, List<string> tags)
        {
            IsTerminal = true;
            Tag = "VERB";
            Word = new InputWord(english, PartOfSpeech.VERB, tags);
        }

        public Verb(string english, string tags) : this(english, tags.Split(',').ToList()) { }

        public override List<InputWord> Flatten()
        {
            return new List<InputWord>() { Word };
        }
    }
}
