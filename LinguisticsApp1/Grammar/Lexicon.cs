using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar
{
    class Lexicon : IEnumerable<Lexeme>
    {
        private List<Lexeme> Lexemes;

        public Lexicon()
        {
            Lexemes = new List<Lexeme>();
        }

        public Lexeme this[string englishLemma]
        {
            get
            {
                return Lexemes.Find(x => x.English == englishLemma);
            }
        }

        public void Add(Lexeme lexeme)
        {
            Lexemes.Add(lexeme);
        }

        public void Add(string english, Phonology.Word lemma, PartOfSpeech pos)
        {
            this.Add(new Lexeme(english, lemma, pos));
        }

        public bool Contains(string english)
        {
            return Lexemes.Exists(x => x.English == english);
        }

        public IEnumerator<Lexeme> GetEnumerator()
        {
            return Lexemes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }
    }
}
