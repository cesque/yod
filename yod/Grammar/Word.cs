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
            Tags.RemoveAll(x => x.Trim().Length == 0);
        }

        public void Inflect(Inflection inflection)
        {
            if (POS == inflection.POS && inflection.Tags.All(x => Tags.Contains(x)))
            {
                if(Inflected == null) Inflected = new Phonology.Word(Lemma);
                if(inflection.Suffix != null) Inflected.Syllables.Add(inflection.Suffix);
            }

            Inflected.ReapplyStress();
        }

        // todo: handle inflection heirarchy in better way
        public void Inflect(List<Inflection> inflections)
        {
            // get the specificity of the highest applicable inflection
            var applicableInflections = inflections.Where(x => x.Tags.All(y => Tags.Contains(y))).ToList();
            if (applicableInflections.Count > 0)
            {
                var maxSpecificity = applicableInflections.Max(x => x.Specificity);
                inflections.Where(x => x.Specificity == maxSpecificity).ToList().ForEach(Inflect);
            }           
        }

        public void Fill(Phonology.Word lemma)
        {
            Lemma = new Phonology.Word(lemma);
            Inflected = new Phonology.Word(lemma);
        }
    }
}
