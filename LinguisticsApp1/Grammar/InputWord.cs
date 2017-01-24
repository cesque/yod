using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar
{
    public class InputWord
    {
        public string EnglishLemma;
        public List<string> Tags;
        public List<InputWord> SubWords;
        public PartOfSpeech POS;

        public InputWord(string lemma, PartOfSpeech pos)
        {
            POS = pos;
            EnglishLemma = lemma;
            Tags = new List<string>();
            SubWords = new List<InputWord>();
        }

        public InputWord(string lemma, PartOfSpeech pos, List<string> tags) : this(lemma, pos)
        {
            Tags = tags;
        }

        public InputWord(string lemma, PartOfSpeech pos, string tags) : this(lemma, pos)
        {
            Tags = tags.Split(',').ToList();
        }

        public void AddWord(InputWord word)
        {
            SubWords.Add(word);
        }
    }
}
