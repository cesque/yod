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
            bool loadPhonology = false;
            string phonologyPath = "";

            LanguageOrthography orthography = null;
            bool loadOrthography = false;
            string orthographyPath = "";

            LanguageGrammar grammar = null;
            bool loadGrammar = false;
            string grammarPath = "";

            Lexicon lexicon = null;
            bool loadLexicon = false;
            string lexiconPath = "";

            Phrase phrase = null;
            bool loadPhrase = false;
            string phrasePath = "";

            var p = new OptionSet()
            {
                {
                    "seed=", "set {SEED} of random number generator.", s => Globals.Seed = Int32.Parse(s)
                },
                {
                    "p|phonology=", "load {PHONOLOGY} from JSON file.", s =>
                    {
                        loadPhonology = true;
                        phonologyPath = s;
                    }
                },
                {
                    "o|orthography=", "load {ORTHOGRAPHY} from JSON file.", s =>
                    {
                        loadOrthography = true;
                        orthographyPath = s;
                    }
                },
                {
                    "l|lexicon=", "load {LEXICON} from JSON file.", s =>
                    {
                        loadLexicon = true;
                        lexiconPath = s;
                    }
                },
                {
                    "g|grammar=", "load {GRAMMAR} from json file", s =>
                    {
                        loadGrammar = true;
                        grammarPath = s;
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
                Console.WriteLine("Seed: " + Globals.Seed);
            }
            catch (OptionException e)
            {
                Console.Write("yodc: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `yodc --help' for more information.");
                return;
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

            try
            {
                if (loadPhonology) phonology = LanguagePhonology.FromJSON(phonologyPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load phonology!");
                return;
            }

            try
            {
                phonology = phonology ?? LanguagePhonology.Generate();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to generate phonology!");
                return;
            }

            try
            {
                if (loadOrthography)
                {
                    orthography = LanguageOrthography.FromJSON(orthographyPath, phonology);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load orthography!");
                return;
            }

            try
            {
                orthography = orthography ?? LanguageOrthography.Generate(phonology);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to generate orthography!");
                return;
            }
            try
            {
                if (loadGrammar) grammar = LanguageGrammar.FromJSON(grammarPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load grammar!");
                return;
            }
            try
            {
                grammar = grammar ?? LanguageGrammar.Generate();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to generate grammar!");
                return;
            }

            try
            {
                if (loadLexicon)
                {
                    lexicon = new Lexicon();
                    lexicon.Fill(lexiconPath, phonology);
                }
            }
            catch (FailedToBuildLexiconException e)
            {
                Console.WriteLine("Failed to build lexicon!");
                return;
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

                try
                {
                    phrase = new Phrase(grammar, phrasePath);
                    phrase.Fill(lexicon);
                }
                catch (LexemeNotFoundException e)
                {
                    Console.WriteLine("Failed to build phrase - lexeme not found in lexicon!");
                    Console.WriteLine("Check that your supplied dictionary contains an entry for all words in the phrase.");
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to build phrase!");
                    return;
                }
            }

            Directory.CreateDirectory("output");

            try
            {
                File.Delete("output/phonology.json");
                File.Delete("output/orthography.json");
                File.Delete("output/grammar.json");
                File.Delete("output/lexicon.txt");
                File.Delete("output/translate.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine("[warning] Failed to delete existing output files.");
            }

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
                File.WriteAllText("output/lexicon.txt", lexicon.ToString(orthography));
                Console.WriteLine("Wrote output/lexicon.txt");
            }
            if (phrase != null)
            {
                File.WriteAllText("output/translated.txt", phrase.ToString(orthography));
                Console.WriteLine("Wrote output/translated.txt");
            }
        }
    }
}