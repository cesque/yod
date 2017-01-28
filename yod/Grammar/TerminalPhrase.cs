using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar
{
    public abstract class TerminalPhrase : Phrase
    {
        public Word Word;

        public TerminalPhrase(string english, PartOfSpeech pos, List<string> tags)
        {
            Word = new Word(english, pos, tags);
        }

        public TerminalPhrase(string english, PartOfSpeech pos, string tags)
        {
            Word = new Word(english, pos, tags.Split(',').ToList());
        }

        public override void Fill(Lexicon lexicon)
        {
            if (lexicon.Any(x => x.English == Word.EnglishLemma && x.POS == Word.POS))
            {
                var lex = lexicon.First(x => x.English == Word.EnglishLemma && x.POS == Word.POS);
                Word.Phonemes = lex.Lemma;
            }
        }

        public override List<Word> Flatten()
        {
            return new List<Word>() { Word };
        }
    }
}
