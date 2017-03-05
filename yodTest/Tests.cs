using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        Lexicon lexicon;

        public Tests()
        {
            //Globals.SeedRandom(4);

            var syllableStructure = new SyllableStructure
            {
                OnsetStructure = new List<SyllableStructure.SyllableStructureOption>
                {
                    new SyllableStructure.SyllableStructureOption(1, 1f)
                },
                NucleusStructure = new List<SyllableStructure.SyllableStructureOption>
                {
                    new SyllableStructure.SyllableStructureOption(1, 1f)
                },
                CodaStructure = new List<SyllableStructure.SyllableStructureOption>
                {
                    new SyllableStructure.SyllableStructureOption(0, 0.7f),
                    new SyllableStructure.SyllableStructureOption(1, 0.7f)
                }
            };


            phonology = new LanguagePhonology(syllableStructure); //new Language(syllableStructure);
            //orthography = new LanguageOrthography(LanguageOrthography.DefaultOrthography, phonology);
            phonology.WordLengthMin = 1;
            phonology.WordLengthMax = 3;

            //var s = "";

            lexicon = new Lexicon();
            lexicon.Fill("./dictionary.json", phonology);
        }

        public string TestPhrase()
        {
            Phrase phrase = new Phrase("./rules3.json", "./inflectioninput.json");
            phrase.Fill(lexicon);

            var flattened = phrase.Flatten();

            var s = "";

            flattened.ForEach(x => s += x.EnglishLemma + " ");
            s += Environment.NewLine;
            s += "/";
            flattened.ForEach(x => s += x.Lemma.ToString() + " ");
            s += "/";

            return s;
        }

        public string TestInflectedPhrase()
        {
            Phrase phrase = new Phrase("./rules3.json", "./inflectioninput.json");
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
            Phrase birthday1 = new Phrase("./rules3.json", "./birthdayinput1.json");
            Phrase birthday2 = new Phrase("./rules3.json", "./birthdayinput2.json");

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

            var line1 = String.Join(" ", flattened1.Select(x => x.Inflected.ToString()));
            var line2 = String.Join(" ", flattened2.Select(x => x.Inflected.ToString()));

            var s = "";
            s += line1 + Environment.NewLine;
            s += line1 + Environment.NewLine;
            s += line2 + Environment.NewLine;
            s += line1 + Environment.NewLine;
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
                //TestLexiconOrthographized
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