using System.Collections.Generic;
using System.Linq;

namespace yod.Phonology
{
    public class Word
    {
        readonly LanguagePhonology language;

        public int SyllableLength => Syllables.Count;
        public List<Syllable> Syllables;

        public Word(Word w)
        {
            language = w.language;
            Syllables = new List<Syllable>();
            w.Syllables.ForEach(x => Syllables.Add(new Syllable(x)));
        }

        public Word(LanguagePhonology language)
        {
            this.language = language;
            Generate();
        }

        public Word(LanguagePhonology language, int syllableLength)
        {
            this.language = language;
            Generate(syllableLength);
        }

        public void Generate()
        {
            var syllableLength = language.WordLengthMin + Globals.Random.Next(language.WordLengthMax - language.WordLengthMin);
            Generate(syllableLength);
        }

        public void Generate(int syllableLength)
        {
            Syllables = new List<Syllable>();
            for (var i = 0; i < syllableLength; i++)
            {
                var syl = new Syllable(language);
                if (Syllables.Count == 0)
                {
                    Syllables.Add(syl);
                }
                else
                {
                    while (Syllables.Last().Phonemes.Last() == syl.Phonemes.First()) syl = new Syllable(language);
                    Syllables.Add(syl);
                }
            }
        }

        public override string ToString()
        {
            var s = "";
            Syllables.ForEach(x => s += x.ToString());
            return s;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var word = (Word) obj;
            if (word.SyllableLength != SyllableLength) return false;
            for (var i = 0; i < SyllableLength; i++)
            {
                if (!word.Syllables[i].Equals(Syllables[i])) return false;
            }
            return true;
        }
    }
}
