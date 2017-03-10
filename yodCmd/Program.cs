using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDesk.Options;
using yod;
using yod.Grammar;
using yod.Orthography;
using yod.Phonology;

namespace yodCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showHelp = false;

            LanguagePhonology phonology = null;
            LanguageOrthography orthography = null;
            bool loadOrthography = false;
            string orthographyPath = "";
            LanguageGrammar grammar = null;

            Lexicon lexicon = null;
            bool loadLexicon = false;
            string lexiconPath = "";

            Phrase phrase = null;
            bool loadPhrase = false;
            string phrasePath = "";

            var p = new OptionSet()
            {
                {
                    "p|phonology=", "load {PHONOLOGY} from JSON file.", s => phonology = LanguagePhonology.FromJSON(s)
                },
                {
                    "o|orthography=", "load {ORTHOGRAPHY} from JSON file.", s =>
                    {
                        loadOrthography = true;
                        orthographyPath = s;
                    }
                },
                {
                    "g|grammar=", "load {GRAMMAR} from json file", s => grammar = LanguageGrammar.FromJSON(s)
                },
                {
                    "l|lexicon=", "load {LEXICON} from JSON file.", s =>
                    {
                        loadLexicon = true;
                        lexiconPath = s;
                    }
                },
                {
                    "s|sentence=|phrase=", "load a sentence or {PHRASE} from JSON file.", s =>
                    {
                        loadPhrase = true;
                        phrasePath = s;
                    }
                },
                {
                    "h|help", "show this message and exit", s => showHelp = true
                }
            };

            var extra = new List<string>();
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("yodc: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `yodc --help' for more information.");
            }

            if (showHelp)
            {
                Console.WriteLine("Usage: yodc [options]");
                Console.WriteLine();
                Console.WriteLine("options:");
                p.WriteOptionDescriptions(Console.Out);
                Console.WriteLine();
                return;
            }

            phonology = phonology ?? LanguagePhonology.Generate();

            if (loadOrthography)
            {
                orthography = LanguageOrthography.FromJSON(orthographyPath, phonology);
            }

            orthography = orthography ?? LanguageOrthography.Generate(phonology);

            if (loadLexicon)
            {
                lexicon = new Lexicon();
                lexicon.Fill(lexiconPath, phonology);
            }

            if (loadPhrase)
            {
                if (grammar == null || lexicon == null)
                {
                    Console.Write("yodc: ");
                    Console.WriteLine("you must supply a phonology, orthography, lexicon and grammar to load and translate a phrase.");
                    Console.WriteLine("Try `yodc --help' for more information.");
                    return;
                }

                phrase = new Phrase(grammar, phrasePath);
                phrase.Fill(lexicon);
            }

            Directory.CreateDirectory("output");
            File.WriteAllText("output/phonology.json", phonology.ToJSON().ToString());
            Console.WriteLine("Wrote output/phonology.json");
            File.WriteAllText("output/orthography.json", orthography.ToJSON().ToString());
            Console.WriteLine("Wrote output/orthography.json");
            if (grammar != null)
            {
                File.WriteAllText("output/grammar.json", grammar.ToJSON().ToString());
                Console.WriteLine("Wrote output/grammar.json");
            }
            if (lexicon != null)
            {
                File.WriteAllText("output/lexicon.txt", lexicon.ToString());
                Console.WriteLine("Wrote output/lexicon.txt");
            }
            if (phrase != null)
            {
                File.WriteAllText("output/translated.txt", phrase.ToString());
                Console.WriteLine("Wrote output/translated.txt");
            }
        }
    }
}