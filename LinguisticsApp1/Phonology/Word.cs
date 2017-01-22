using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Phonology
{
    public class Word
    {
        LanguagePhonology language;
        public int SyllableLength;
        public List<Syllable> Syllables;

        public Word(LanguagePhonology l)
        {
            language = l;
            Generate();
        }

        public void Generate()
        {
            Syllables = new List<Syllable>();
            SyllableLength = language.WordLengthMin + Globals.Random.Next(language.WordLengthMax - language.WordLengthMin);
            for(var i = 0; i<SyllableLength; i++)
            {
                var syl = new Syllable(language);
                if(Syllables.Count == 0)
                {
                    Syllables.Add(syl);
                } else
                {
                    while(Syllables.Last().Phonemes.Last() == syl.Phonemes.First()) syl = new Syllable(language);
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
    }
}
