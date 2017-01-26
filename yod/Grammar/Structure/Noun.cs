using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Noun : GrammarPhrase
    {
        public InputWord Word;

        public Noun(string english, List<string> tags)
        {
            IsTerminal = true;
            Tag = "NOUN";
            Word = new InputWord(english, PartOfSpeech.NOUN, tags);
        }

        public Noun(string english, string tags) : this(english, tags.Split(',').ToList()) { }

        public override List<InputWord> Flatten()
        {
            return new List<InputWord>() { Word };
        }
    }
}
