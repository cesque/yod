using System.Collections.Generic;
using yod.Phonology;

namespace yod.Grammar
{
    public class Inflection
    {
        public PartOfSpeech POS;
        public List<string> Tags;
        public Syllable Suffix;
        public int Specificity => Tags.Count;

        public Inflection(LanguagePhonology phonology, PartOfSpeech pos, string tag) : this(phonology, pos, new List<string> { tag }) { }
        public Inflection(LanguagePhonology phonology, PartOfSpeech pos, List<string> tags)
        {
            POS = pos;
            Tags = tags;

            Suffix = phonology.GetSyllable();
        }
    }
}
