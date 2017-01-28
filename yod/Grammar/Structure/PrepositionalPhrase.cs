using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class PrepositionalPhrase : NonTerminalPhrase
    {
        public Preposition Prep
        {
            get
            {
                if (!prepositionEnabled) throw new Exception("Prep is disabled for rule " + Rule);
                return preposition;
            }
            set
            {
                if (!prepositionEnabled) throw new Exception("Prep is disabled for rule " + Rule);
                preposition = value;
            }
        }
        private Preposition preposition;
        private bool prepositionEnabled;

        public NounPhrase NP
        {
            get
            {
                if (!nounPhraseEnabled) throw new Exception("NP is disabled for rule " + Rule);
                return nounPhrase;
            }
            set
            {
                if (!nounPhraseEnabled) throw new Exception("NP is disabled for rule " + Rule);
                nounPhrase = value;
            }
        }
        private NounPhrase nounPhrase;
        private bool nounPhraseEnabled;

        public PrepositionalPhrase(string rule) : base(rule)
        {
            prepositionEnabled = false;
            nounPhraseEnabled = false;

            switch (Rule)
            {
                case "Prep NP":
                    prepositionEnabled = true;
                    nounPhraseEnabled = true;
                    break;
                default:
                    throw new Exception("Unrecognised rule " + Rule + "!");
            }
        }

        public override void Fill(Lexicon lexicon)
        {
            switch (Rule)
            {
                case "Prep NP":
                    Prep.Fill(lexicon);
                    NP.Fill(lexicon);
                    break;
                default: throw new Exception("Couldn't find current rule! Probably a typo here.");
            }
        }

        public override List<Word> Flatten()
        {
            switch (Rule)
            {
                case "Prep NP": return Prep.Flatten().Concat(NP.Flatten()).ToList();
                default: throw new Exception("Couldn't find current rule! Probably a typo here.");
            }
        }
    }
}
