using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class VerbPhrase : NonTerminalPhrase
    {
        public Verb V
        {
            get
            {
                if (!verbEnabled) throw new Exception("V is disabled for rule " + Rule);
                return verb;
            }
            set
            {
                if (!verbEnabled) throw new Exception("V is disabled for rule " + Rule);
                verb = value;
            }
        }
        private Verb verb;
        private bool verbEnabled;

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

        public Adjective Adj
        {
            get
            {
                if (!adjectiveEnabled) throw new Exception("Adj is disabled for rule " + Rule);
                return adjective;
            }
            set
            {
                if (!adjectiveEnabled) throw new Exception("Adj is disabled for rule " + Rule);
                adjective = value;
            }
        }
        private Adjective adjective;
        private bool adjectiveEnabled;

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

        public Adverb Adv
        {
            get
            {
                if (!adverbEnabled) throw new Exception("Adv is disabled for rule " + Rule);
                return adverb;
            }
            set
            {
                if (!adverbEnabled) throw new Exception("Adv is disabled for rule " + Rule);
                adverb = value;
            }
        }
        private Adverb adverb;
        private bool adverbEnabled;

        public VerbPhrase(string rule) : base(rule)
        {
            verbEnabled = false;
            nounPhraseEnabled = false;
            adjectiveEnabled = false;
            prepositionalPhraseEnabled = false;
            adverbEnabled = false;

            switch (Rule)
            {
                case "V":
                    verbEnabled = true;
                    break;
                case "V NP":
                    verbEnabled = true;
                    nounPhraseEnabled = true;
                    break;
                case "V Adj":
                    verbEnabled = true;
                    adjectiveEnabled = true;
                    break;
                case "V PP":
                    verbEnabled = true;
                    prepositionalPhraseEnabled = true;
                    break;
                case "V Adv":
                    verbEnabled = true;
                    adverbEnabled = true;
                    break;
                default:
                    throw new Exception("Unrecognised rule " + Rule + "!");
            }
        }

        public override void Fill(Lexicon lexicon)
        {
            switch (Rule)
            {
                case "V":
                    V.Fill(lexicon);
                    break;
                case "V NP":
                    V.Fill(lexicon);
                    NP.Fill(lexicon);
                    break;
                case "V Adj":
                    V.Fill(lexicon);
                    Adj.Fill(lexicon);
                    break;
                case "V PP":
                    V.Fill(lexicon);
                    PP.Fill(lexicon);
                    break;
                case "V Adv":
                    V.Fill(lexicon);
                    Adv.Fill(lexicon);
                    break;
                default: throw new Exception("Couldn't find current rule! Probably a typo here.");
            }
        }

        public override List<Word> Flatten()
        {
            switch(Rule)
            {
                case "V": return V.Flatten();
                case "V NP": return V.Flatten().Concat(NP.Flatten()).ToList();
                case "V Adj": return V.Flatten().Concat(Adj.Flatten()).ToList();
                case "V PP": return V.Flatten().Concat(PP.Flatten()).ToList();
                case "V Adv": return V.Flatten().Concat(Adv.Flatten()).ToList();
                default: throw new Exception("Couldn't find current rule! Probably a typo here.");
            }
        }
    }
}
