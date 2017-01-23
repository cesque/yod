using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

using yod;
using yod.Phonology;
using yod.Orthography;
using yod.Grammar;

namespace yodTest
{
    class Program
    {
        static void Main(string[] args)
        {
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


            var language = new LanguagePhonology(syllableStructure);//new Language(syllableStructure);
            var orthography = new LanguageOrthography(LanguageOrthography.DefaultOrthography, language);
            language.WordLengthMin = 1;
            language.WordLengthMax = 4;
            language.OnsetConsonants = new List<Consonant>(language.Phonemes.Consonants.Where(
                c => true
            ));
            language.CodaConsonants = new List<Consonant>(language.Phonemes.Consonants.Where(
                c => true
            ));

            var s = "";

            Lexicon lexicon = new Lexicon();
            lexicon.Fill("./input.txt", language);

            var maxEnglish = lexicon.Max(x => x.English.Length);
            foreach(var lexeme in lexicon)
            {
                s += lexeme.English.PadLeft(maxEnglish) + " : " + lexeme.Lemma.ToString() + Environment.NewLine;
            }

            File.WriteAllText("./words.txt", s);
            Process.Start("notepad.exe", "./words.txt");


        }
    }
}
