using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Pronoun : GrammarPhrase
    {
        public InputWord Word;

        public Pronoun(string english, List<string> tags)
        {
            IsTerminal = true;
            Tag = "PRON";
            Word = new InputWord(english, PartOfSpeech.PRONOUN, tags);
        }

        public Pronoun(string english, string tags) : this(english, tags.Split(',').ToList()) { }

        public override List<InputWord> Flatten()
        {
            return new List<InputWord>() { Word };
        }
    }
}
