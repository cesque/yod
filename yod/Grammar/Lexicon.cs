using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace yod.Grammar
{
    public class Lexicon : IEnumerable<Lexeme>
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

        public void Fill(List<Tuple<string, PartOfSpeech>> words, Phonology.LanguagePhonology phonology)
        {
            foreach (var tuple in words)
            {
                var english = tuple.Item1;
                var pos = tuple.Item2;

                this.Add(english, phonology.GetWord(), pos);
            }
        }

        public void Fill(string filepath, Phonology.LanguagePhonology phonology)
        {
            var posDict = new Dictionary<string, PartOfSpeech>()
            {
                {"PRON", PartOfSpeech.PRONOUN},
                {"NOUN", PartOfSpeech.NOUN },
                {"VERB", PartOfSpeech.VERB },
                {"ADVB", PartOfSpeech.ADVERB },
                {"ADJC", PartOfSpeech.ADJECTIVE },
                {"CONJ", PartOfSpeech.CONJUNCTION },
                {"PREP", PartOfSpeech.PREPOSITION },
                {"INTJ", PartOfSpeech.INTERJECTION },
                {"DETM", PartOfSpeech.DETERMINER }
            };

            var words = new List<Tuple<string, PartOfSpeech>>();

            var lines = File.ReadAllLines(filepath).ToList();
            foreach (var line in lines)
            {
                if (String.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(':').ToList();
                if (posDict.ContainsKey(parts[0].Trim()))
                {
                    var pos = posDict[parts[0].Trim()];
                    var word = parts[1].Trim();
                    words.Add(new Tuple<string, PartOfSpeech>(word, pos));
                }
                else
                {
                    throw new Exception("Part of speech " + parts[0] + " not found.");
                }
            }

            this.Fill(words, phonology);
        }
    }
}
