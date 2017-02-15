using System;
using System.Collections.Generic;
using yod.Phonology;

namespace yod.Orthography
{
    public class LanguageOrthography
    {
        public Dictionary<string, string> Rules;
        LanguagePhonology language;

        public static readonly Dictionary<string, string> DefaultOrthography = new Dictionary<string, string>
        {
            #region rules
            { "m", "m" },
            { "n", "n" },
            { "ɲ", "ny" },
            { "ŋ", "ng" },
            { "p", "p" },
            { "b", "b" },
            { "t", "t" },
            { "d", "d" },
            { "k", "k" },
            { "g", "g" },
            { "ʔ", "-" },
            { "t͡s", "ts" },
            { "d͡z", "dz" },
            { "t͡ʃ", "ch" },
            { "d͡ʒ", "dzh" },
            { "ʈ͡ʂ", "tsh" },
            { "ɖ͡ʐ", "dzh" },
            { "t͡ɕ", "tsh" },
            { "d͡ʑ", "dzh" },
            { "t͡θ", "th" },
            { "d͡ð", "th" },
            { "s", "s" },
            { "z", "z" },
            { "ʃ", "sh" },
            { "ʒ", "zh" },
            { "ʂ", "sh" },
            { "ʐ", "zh" },
            { "ɕ", "sh" },
            { "ʑ", "zh" },
            { "f", "f" },
            { "v", "v" },
            { "θ", "th" },
            { "ð", "th" },
            { "x", "h" },
            { "h", "h" },
            { "ʋ", "v" },
            { "ɹ", "r" },
            { "j", "y" },
            { "ʍ", "wh" },
            { "w", "w" },
            { "B", "br" },
            { "r", "r" },
            { "R", "rr" },
            { "l", "l" },
            { "ʎ", "ly" },
            { "a", "a" },
            { "e", "e" },
            { "i", "i" },
            { "o", "o" },
            { "u", "u" },
            #endregion
        };

        public static readonly Dictionary<string, string> CroatianOrthography = new Dictionary<string, string>
        {
            #region rules
	        { "a", "a" },
            { "b", "b" },
            { "v", "v" },
            { "g", "g" },
            { "d", "d" },
            { "d͡ʑ", "đ" },
            { "e", "e" },
            { "ʐ", "ž" },
            { "z", "z" },
            { "i", "i" },
            { "j", "j" },
            { "k", "k" },
            { "l", "l" },
            { "ʎ", "lj" },
            { "m", "m" },
            { "n", "n" },
            { "ɲ", "nj" },
            { "p", "p" },
            { "r", "r" },
            { "s", "s" },
            { "t", "t" },
            { "t͡ɕ", "ć" },
            { "u", "u" },
            { "f", "f" },
            { "x", "h" },
            { "t͡s", "c"},
            { "ʈ͡ʂ", "č"},
            { "ɖ͡ʐ", "dž"},
            { "ʂ", "š"}
            #endregion
        };

        public LanguageOrthography(LanguagePhonology lang) : this(DefaultOrthography, lang)
        {

        }

        public LanguageOrthography(Dictionary<string, string> rules, LanguagePhonology lang)
        {
            Rules = rules;
            language = lang;

            foreach (var c in language.Phonemes.Consonants)
            {
                if (!rules.ContainsKey(c.Symbol)) throw new Exception("Orthography does not contain a grapheme for the (consonant) phoneme [" + c.Symbol + "]");
            }
            foreach (var v in language.Phonemes.Vowels)
            {
                if (!rules.ContainsKey(v.Symbol)) throw new Exception("Orthography does not contain a grapheme for the (vowel) phoneme [" + v.Symbol + "]");
            }
        }

        public string Orthographize(Phoneme phoneme)
        {
            return Rules[phoneme.Symbol];
        }

        public string Orthographize(Syllable syllable)
        {
            var s = "";
            foreach (var phoneme in syllable.Phonemes)
            {
                s += Orthographize(phoneme);
            }
            return s;
        }

        public string Orthographize(Word word)
        {
            var s = "";
            foreach (var syllable in word.Syllables)
            {
                s += Orthographize(syllable);
            }
            return s;
        }
    }
}
