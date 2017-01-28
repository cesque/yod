using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class RelativeClause : NonTerminalPhrase
    {
        public Relativizer Rel
        {
            get
            {
                if (!relativizerEnabled) throw new Exception("Rel is disabled for rule " + Rule);
                return relativizer;
            }
            set
            {
                if (!relativizerEnabled) throw new Exception("Rel is disabled for rule " + Rule);
                relativizer = value;
            }
        }
        private Relativizer relativizer;
        private bool relativizerEnabled;

        public VerbPhrase VP
        {
            get
            {
                if (!verbPhraseEnabled) throw new Exception("VP is disabled for rule " + Rule);
                return verbPhrase;
            }
            set
            {
                if (!verbPhraseEnabled) throw new Exception("VP is disabled for rule " + Rule);
                verbPhrase = value;
            }
        }
        private VerbPhrase verbPhrase;
        private bool verbPhraseEnabled;

        public RelativeClause(string rule) : base(rule)
        {
            relativizerEnabled = false;
            verbPhraseEnabled = false;

            switch (Rule)
            {
                case "Rel VP":
                    relativizerEnabled = true;
                    verbPhraseEnabled = true;
                    break;
                default:
                    throw new Exception("Unrecognised rule " + Rule + "!");
            }
        }

        public override void Fill(Lexicon lexicon)
        {
            switch (Rule)
            {
                case "Rel VP":
                    Rel.Fill(lexicon);
                    VP.Fill(lexicon);
                    break;
                default: throw new Exception("Couldn't find current rule! Probably a typo here.");
            }
        }

        public override List<Word> Flatten()
        {
            switch (Rule)
            {
                case "Rel VP": return Rel.Flatten().Concat(VP.Flatten()).ToList();
                default: throw new Exception("Couldn't find current rule! Probably a typo here.");
            }
        }
    }
}
