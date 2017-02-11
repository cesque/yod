using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;

using yod;
using yod.Phonology;
using yod.Orthography;
using yod.Grammar;
using yod.Grammar.Structure;
using System.Diagnostics;

namespace yodTest
{
    public class Tests
    {
        LanguagePhonology phonology;
        LanguageOrthography orthography;
        Lexicon lexicon;

        public Tests()
        {
            Globals.SeedRandom(3);

            var syllableStructure = new SyllableStructure()
            {
                OnsetStructure = new List<SyllableStructure.SyllableStructureOption>() {
                    new SyllableStructure.SyllableStructureOption(1, 1f),
                },
                NucleusStructure = new List<SyllableStructure.SyllableStructureOption>() {
                    new SyllableStructure.SyllableStructureOption(1, 1f),
                },
                CodaStructure = new List<SyllableStructure.SyllableStructureOption>() {
                    new SyllableStructure.SyllableStructureOption(0, 0.3f),
                    new SyllableStructure.SyllableStructureOption(1, 0.7f),
                }
            };


            phonology = new LanguagePhonology(syllableStructure);//new Language(syllableStructure);
            orthography = new LanguageOrthography(LanguageOrthography.DefaultOrthography, phonology);
            phonology.WordLengthMin = 1;
            phonology.WordLengthMax = 4;
            phonology.OnsetConsonants = new List<Consonant>(phonology.Phonemes.Consonants.Where(
                c => true
            ));
            phonology.CodaConsonants = new List<Consonant>(phonology.Phonemes.Consonants.Where(
                c => true
            ));

            //var s = "";

            lexicon = new Lexicon();
            lexicon.Fill("./dictionary.txt", phonology);
        }

        public string TestPhrase()
        {
            Phrase phrase = new Phrase("./rules.json", "./input.json");
            phrase.Fill(lexicon);

            var flattened = phrase.Flatten();

            var s = "";

            flattened.ForEach(x => s += x.EnglishLemma + " ");
            s += Environment.NewLine;
            s += "/";
            flattened.ForEach(x => s += x.Phonemes.ToString() + " ");
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

        public void Run()
        {
            var s = "";

            s += TestPhrase();
            s += Environment.NewLine + Environment.NewLine;
            s += TestLexiconOrthographized();

            File.WriteAllText("./output.txt", s);
            Process.Start("notepad.exe", "./output.txt");
        }
    }
}
