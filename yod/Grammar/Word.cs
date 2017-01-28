using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar
{
    public class Word
    {
        public yod.Phonology.Word Phonemes;
        public string EnglishLemma;
        public List<string> Tags;
        public PartOfSpeech POS;

        public Word(string lemma, PartOfSpeech pos)
        {
            POS = pos;
            EnglishLemma = lemma;
            Tags = new List<string>();
        }

        public Word(string lemma, PartOfSpeech pos, List<string> tags) : this(lemma, pos)
        {
            Tags = tags;
        }

        public Word(string lemma, PartOfSpeech pos, string tags) : this(lemma, pos)
        {
            Tags = tags.Split(',').ToList();
        }
    }
}
