using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

using yod;

namespace yodTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var syllableStructure = new SyllableStructure()
            {
                OnsetStructure = new List<SyllableStructure.SyllableStructureOption>() { new SyllableStructure.SyllableStructureOption(1, 1) },
                NucleusStructure = new List<SyllableStructure.SyllableStructureOption>() { new SyllableStructure.SyllableStructureOption(1, 1) },
                CodaStructure = new List<SyllableStructure.SyllableStructureOption>() { new SyllableStructure.SyllableStructureOption(1, 1) }
            };*/

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


            var language = new Language(syllableStructure);//new Language(syllableStructure);
            var orthography = new Orthography(Orthography.DefaultOrthography, language);
            language.WordLengthMin = 1;
            language.WordLengthMax = 4;
            language.OnsetConsonants = new List<Consonant>(language.Phonemes.Consonants.Where(
                c => true
            ));
            language.CodaConsonants = new List<Consonant>(language.Phonemes.Consonants.Where(
                c => true
            ));

            var s = "";

            var line1 = "  ";
            var line2 = "[ ";

            for (var i = 0; i < 14; i++)
            {
                var word1 = language.GetWord();
                var word2 = orthography.Orthographize(word1);

                var length1 = Language.GetCharacterLength(word1.ToString());
                var length2 = word2.Length;
                var maxlength = Math.Max(length1, length2);

                line1 += word2.PadRight(maxlength) + " ";
                line2 += word1.ToString().PadRight(maxlength + (word1.ToString().Length - length1)) + " ";

                var a = word1.ToString().ToCharArray();
            }

            s = line1 + Environment.NewLine + line2 + "]" + Environment.NewLine;

            foreach (var phoneme in language.Phonemes.AllPhonemes)
            {
                //s += phoneme.Symbol + " ";
            }

            File.WriteAllText("./words.txt", s);
            Process.Start("notepad.exe", "./words.txt");
        }
    }
}
