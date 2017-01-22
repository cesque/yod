using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using yod;

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
