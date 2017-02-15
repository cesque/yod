namespace yod.Grammar
{
    public class Lexeme
    {
        public Phonology.Word Lemma;
        public string English;
        public PartOfSpeech POS;

        public Lexeme(string english, Phonology.Word lemma, PartOfSpeech pos) 
        {
            English = english;
            Lemma = lemma;
            POS = pos;
        }
    }
}
