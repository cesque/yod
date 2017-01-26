using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Preposition : GrammarPhrase
    {
        public InputWord Word;

        public Preposition(string english, List<string> tags)
        {
            IsTerminal = true;
            Tag = "PREP";
            Word = new InputWord(english, PartOfSpeech.NOUN, tags);
        }

        public Preposition(string english, string tags) : this(english, tags.Split(',').ToList()) { }

        public override List<InputWord> Flatten()
        {
            return new List<InputWord>() { Word };
        }
    }
}
