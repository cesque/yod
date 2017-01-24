using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar
{
    public class Sentence
    {
        public List<Word> Words;

        public Sentence(Lexicon lexicon, Phonology.LanguagePhonology phonology)
        {
            Words = new List<Word>();
        }

        public Sentence(InputSentence input, Lexicon lexicon, Phonology.LanguagePhonology phonology) : this(lexicon, phonology)
        {
            var order = input.Order;
            InputWord w1 = null;
            InputWord w2 = null;
            InputWord w3 = null;
            if (order == SentenceOrder.OSV || order == SentenceOrder.OVS)
            {
                w1 = input.Object;
                if (order == SentenceOrder.OSV)
                {
                    w2 = input.Subject;
                    w3 = input.Verb;
                }
                else
                {
                    w2 = input.Verb;
                    w3 = input.Subject;
                }
            }
            else if (order == SentenceOrder.SOV || order == SentenceOrder.SVO)
            {
                w1 = input.Subject;
                if (order == SentenceOrder.SOV)
                {
                    w2 = input.Object;
                    w3 = input.Verb;
                }
                else
                {
                    w2 = input.Verb;
                    w3 = input.Object;
                }
            }
            else if (order == SentenceOrder.VOS || order == SentenceOrder.VSO)
            {
                w1 = input.Verb;
                if (order == SentenceOrder.VOS)
                {
                    w2 = input.Object;
                    w3 = input.Subject;
                }
                else
                {
                    w2 = input.Subject;
                    w3 = input.Object;
                }
            }

            var word1 = new Word(lexicon[w1.EnglishLemma].Lemma, w1.EnglishLemma, w1.Tags);
            var word2 = new Word(lexicon[w2.EnglishLemma].Lemma, w2.EnglishLemma, w2.Tags);
            var word3 = new Word(lexicon[w3.EnglishLemma].Lemma, w3.EnglishLemma, w3.Tags);

            Words.Add(word1);
            Words.Add(word2);
            Words.Add(word3);
        }
    }
}
