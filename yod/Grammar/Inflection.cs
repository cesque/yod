using yod.Phonology;

namespace yod.Grammar
{
    public class Inflection
    {
        public PartOfSpeech POS;
        public string Tag;
        public Syllable Suffix;

        public Inflection(LanguagePhonology phonology, PartOfSpeech pos, string tag)
        {
            POS = pos;
            Tag = tag;

            Suffix = phonology.GetSyllable();
        }
    }
}
