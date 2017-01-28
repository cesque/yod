using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class NounPhrase : NonTerminalPhrase
    {
        public Noun N
        {
            get
            {
                if (!nounEnabled) throw new Exception("N is disabled for rule " + Rule);
                return noun;
            }
            set
            {
                if (!nounEnabled) throw new Exception("N is disabled for rule " + Rule);
                noun = value;
            }
        }
        private Noun noun;
        private bool nounEnabled;

        public Determiner Det
        {
            get
            {
                if (!determinerEnabled) throw new Exception("Det is disabled for rule " + Rule);
                return determiner;
            }
            set
            {
                if (!determinerEnabled) throw new Exception("Det is disabled for rule " + Rule);
                determiner = value;
            }
        }
        private Determiner determiner;
        private bool determinerEnabled;

        public Pronoun Pro
        {
            get
            {
                if (!pronounEnabled) throw new Exception("Pro is disabled for rule " + Rule);
                return pronoun;
            }
            set
            {
                if (!pronounEnabled) throw new Exception("Pro is disabled for rule " + Rule);
                pronoun = value;
            }
        }
        private Pronoun pronoun;
        private bool pronounEnabled;

        public PrepositionalPhrase PP
        {
            get
            {
                if (!prepositionalPhraseEnabled) throw new Exception("PP is disabled for rule " + Rule);
                return prepositionalPhrase;
            }
            set
            {
                if (!prepositionalPhraseEnabled) throw new Exception("PP is disabled for rule " + Rule);
                prepositionalPhrase = value;
            }
        }
        private PrepositionalPhrase prepositionalPhrase;
        private bool prepositionalPhraseEnabled;

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

        public RelativeClause RC
        {
            get
            {
                if (!relativeClauseEnabled) throw new Exception("NP is disabled for rule " + Rule);
                return relativeClause;
            }
            set
            {
                if (!relativeClauseEnabled) throw new Exception("NP is disabled for rule " + Rule);
                relativeClause = value;
            }
        }
        private RelativeClause relativeClause;
        private bool relativeClauseEnabled;

        public NounPhrase(string rule) : base(rule)
        {
            nounEnabled = false;
            determinerEnabled = false;
            pronounEnabled = false;
            nounPhraseEnabled = false;
            prepositionalPhraseEnabled = false;
            relativeClauseEnabled = false;

            switch (Rule)
            {
                case "N":
                    nounEnabled = true;
                    break;
                case "Det N":
                    nounEnabled = true;
                    determinerEnabled = true;
                    break;
                case "Pro":
                    pronounEnabled = true;
                    break;
                case "NP PP":
                    nounPhraseEnabled = true;
                    prepositionalPhraseEnabled = true;
                    break;
                case "NP RC":
                    nounPhraseEnabled = true;
                    relativeClauseEnabled = true;
                    break;
                default:
                    throw new Exception("Unrecognised rule " + Rule + "!");
            }
        }

        public override void Fill(Lexicon lexicon)
        {
            switch (Rule)
            {
                case "N":
                    N.Fill(lexicon);
                    break;
                case "Det N":
                    Det.Fill(lexicon);
                    N.Fill(lexicon);
                    break;
                case "Pro":
                    Pro.Fill(lexicon);
                    break;
                case "NP PP":
                    NP.Fill(lexicon);
                    PP.Fill(lexicon);
                    break;
                case "NP RC":
                    NP.Fill(lexicon);
                    RC.Fill(lexicon);
                    break;
                default: throw new Exception("Couldn't find current rule! Probably a typo here.");
            }
        }

        public override List<Word> Flatten()
        {
            switch (Rule)
            {
                case "Pro": return Pro.Flatten();
                case "N": return N.Flatten();
                case "Det N": return Det.Flatten().Concat(N.Flatten()).ToList();
                case "NP PP": return NP.Flatten().Concat(PP.Flatten()).ToList();
                case "NP RC": return NP.Flatten().Concat(RC.Flatten()).ToList();
                default: throw new Exception("Couldn't find current rule! Probably a typo here.");
            }
        }
    }
}
