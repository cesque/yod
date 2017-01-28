using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Sentence : NonTerminalPhrase
    {
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

        public Conjunction Conj
        {
            get
            {
                if (!conjunctionEnabled) throw new Exception("Conj is disabled for rule " + Rule);
                return conjunction;
            }
            set
            {
                if (!conjunctionEnabled) throw new Exception("Conj is disabled for rule " + Rule);
                conjunction = value;
            }
        }
        private Conjunction conjunction;
        private bool conjunctionEnabled;

        public Sentence S1
        {
            get
            {
                if (!sentence1Enabled) throw new Exception("S(1) is disabled for rule " + Rule);
                return sentence1;
            }
            set
            {
                if (!sentence1Enabled) throw new Exception("S(1) is disabled for rule " + Rule);
                sentence1 = value;
            }
        }
        private Sentence sentence1;
        private bool sentence1Enabled;

        public Sentence S2
        {
            get
            {
                if (!sentence2Enabled) throw new Exception("S(2) is disabled for rule " + Rule);
                return sentence2;
            }
            set
            {
                if (!sentence2Enabled) throw new Exception("S(2) is disabled for rule " + Rule);
                sentence2 = value;
            }
        }
        private Sentence sentence2;
        private bool sentence2Enabled;

        public Sentence(string rule) : base(rule)
        {
            nounPhraseEnabled = false;
            verbPhraseEnabled = false;
            conjunctionEnabled = false;
            sentence1Enabled = false;
            sentence2Enabled = false;

            switch (Rule)
            {
                case "NP VP":
                    nounPhraseEnabled = true;
                    verbPhraseEnabled = true;
                    break;
                case "S Conj S":
                    sentence1Enabled = true;
                    conjunctionEnabled = true;
                    sentence2Enabled = true;
                    break;
                default:
                    throw new Exception("Unrecognised rule " + Rule + "!");
            }
        }

        public override void Fill(Lexicon lexicon)
        {
            switch (Rule)
            {
                case "NP VP":
                    NP.Fill(lexicon);
                    VP.Fill(lexicon);
                    break;
                case "S Conj S":
                    S1.Fill(lexicon);
                    Conj.Fill(lexicon);
                    S2.Fill(lexicon);
                    break;
                default: throw new Exception("Couldn't find current rule! Probably a typo here.");
            }
        }

        public override List<Word> Flatten()
        {
            switch (Rule)
            {
                case "NP VP": return NP.Flatten().Concat(VP.Flatten()).ToList();
                case "S Conj S": return S1.Flatten().Concat(Conj.Flatten().Concat(S2.Flatten())).ToList();
                default: throw new Exception("Couldn't find current rule! Probably a typo here.");
            }           
        }
    }
}
