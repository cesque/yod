using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using yod;
using yod.Grammar;
using yod.Grammar.Structure;
using yod.Orthography;
using yod.Phonology;

namespace yodTest
{
    public class Tests
    {
        LanguagePhonology phonology;
        LanguageOrthography orthography;
        LanguageGrammar grammar;
        
        Lexicon lexicon;

        public Tests()
        {
            //Globals.SeedRandom(4);

            phonology = LanguagePhonology.FromJSON("./english.json");
            orthography = LanguageOrthography.Generate(phonology);
            grammar = LanguageGrammar.FromJSON("./rules3.json");

            lexicon = new Lexicon();
            lexicon.Fill("./dictionary.json", phonology);
        }

        public string TestPhrase()
        {
            Phrase phrase = new Phrase(grammar, "./inflectioninput.json");
            phrase.Fill(lexicon);

            var flattened = phrase.Flatten();

            var s = "";

            flattened.ForEach(x => s += x.EnglishLemma + " ");
            s += Environment.NewLine;
            flattened.ForEach(x => s += orthography.Orthographize(x.Inflected) + " ");
            s += Environment.NewLine;
            s += "/";
            flattened.ForEach(x => s += x.Lemma.ToString() + " ");
            s += "/";

            return s;
        }

        public string TestInflectedPhrase()
        {
            Phrase phrase = new Phrase(grammar, "./inflectioninput.json");
            phrase.Fill(lexicon);

            var subjectSyllable = phonology.GetSyllable();
            var objectSyllable = phonology.GetSyllable();

            List<Inflection> inflections = new List<Inflection>
            {
                new Inflection(phonology, PartOfSpeech.Noun, "GEN"),
                new Inflection(phonology, PartOfSpeech.Noun, "OBJ"),
                new Inflection(phonology, PartOfSpeech.Pronoun, "GEN"),
                new Inflection(phonology, PartOfSpeech.Pronoun, "OBJ"),
            };

            phrase.InflectAll(inflections);
            var flattened = phrase.Flatten();

            var s = "";

            flattened.ForEach(x => s += x.EnglishLemma + " ");
            s += Environment.NewLine;
            flattened.ForEach(x => s += orthography.Orthographize(x.Inflected) + " ");
            s += Environment.NewLine;
            s += "/";
            flattened.ForEach(x => s += x.Inflected.ToString() + " ");
            s += "/";

            return s;
        }

        public string TestLexicon()
        {
            return lexicon.ToString();
        }

        public string TestLexiconOrthographized()
        {
            return lexicon.ToString(orthography);
        }


        public string TestBirthday()
        {
            Phrase birthday1 = new Phrase(grammar, "./birthdayinput1.json");
            Phrase birthday2 = new Phrase(grammar, "./birthdayinput2.json");

            birthday1.Fill(lexicon);
            birthday2.Fill(lexicon);

            List<Inflection> inflections = new List<Inflection>
            {
                new Inflection(phonology, PartOfSpeech.Noun, "OBJ"),
                new Inflection(phonology, PartOfSpeech.Noun, "SBJ"),
                new Inflection(phonology, PartOfSpeech.Pronoun, "SBJ"),
                new Inflection(phonology, PartOfSpeech.Pronoun, "SBJ"),
                new Inflection(phonology, PartOfSpeech.Pronoun, "GEN"),
                new Inflection(phonology, PartOfSpeech.Noun, "GEN"),
            };
            
            birthday1.InflectAll(inflections);
            birthday2.InflectAll(inflections);

            var flattened1 = birthday1.Flatten();
            var flattened2 = birthday2.Flatten();

            var line1 = "/" + String.Join(" ", flattened1.Select(x => x.Inflected.ToString())) + "/";
            var orth1 = String.Join(" ", flattened1.Select(x => orthography.Orthographize(x.Inflected)));
            var line2 = "/" + String.Join(" ", flattened2.Select(x => x.Inflected.ToString())) + "/";
            var orth2 = String.Join(" ", flattened2.Select(x => orthography.Orthographize(x.Inflected)));

            var s = "";
            s += orth1 + Environment.NewLine;
            s += line1 + Environment.NewLine;
            s += orth1 + Environment.NewLine;
            s += line1 + Environment.NewLine;
            s += orth2 + Environment.NewLine;
            s += line2 + Environment.NewLine;
            s += orth1 + Environment.NewLine;
            s += line1 + Environment.NewLine;
            return s;
        }

        public string TestOrthography()
        {
            var s = "";

            foreach (var phoneme in phonology.Phonemes.AllPhonemes)
            {
                var symboldiff = phoneme.Symbol.Length - Globals.StripTies(phoneme.Symbol).Length;
                s += phoneme.Symbol.PadRight(2 + symboldiff) + " -> " + orthography.Orthographize(phoneme) + Environment.NewLine;
            }

            return s;
        }

        public void Run()
        {
            var s = "";

            var tests = new List<Func<string>>
            {
                TestBirthday,

                TestPhrase,
                TestInflectedPhrase,
                TestLexiconOrthographized,
                TestOrthography
            };

            tests.ForEach(test =>
            {
                s += test();
                s += Environment.NewLine + Environment.NewLine;
            });


            File.WriteAllText("./output.txt", s);
            Process.Start("notepad.exe", "./output.txt");
        }
    }
}