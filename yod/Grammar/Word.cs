using System.Collections.Generic;
using System.Linq;

namespace yod.Grammar
{
    public class Word
    {
        public Phonology.Word Lemma;
        public Phonology.Word Inflected;
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

        public void Inflect(Inflection inflection)
        {
            if (POS == inflection.POS && inflection.Tags.All(x => Tags.Contains(x)))
            {
                if(Inflected == null) Inflected = new Phonology.Word(Lemma);
                if(inflection.Suffix != null) Inflected.Syllables.Add(inflection.Suffix);
            }
        }

        // todo: handle inflection heirarchy in better way
        public void Inflect(List<Inflection> inflections)
        {
            inflections.ForEach(Inflect);
        }

        public void Fill(Phonology.Word lemma)
        {
            Lemma = new Phonology.Word(lemma);
            Inflected = new Phonology.Word(lemma);
        }
    }
}
