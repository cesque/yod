using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace yod.Grammar
{
    public class LanguageGrammar : List<PhraseStructureRule>
    {
        public LanguageGrammar()
        {
        }

        public static LanguageGrammar FromJSON(JToken jtoken)
        {
            LanguageGrammar rules = new LanguageGrammar();
            var o = jtoken as JObject;
            var rulesObj = o["rules"] as JObject;

            var compoundRules = o["compound"] as JObject;

            foreach (var v in rulesObj.Properties())
            {
                var ruleName = v.Name;
                var expandsTo = (v.Value as JArray).Values<string>().ToList();
                foreach (var ruleSub in expandsTo)
                {
                    var rule = new PhraseStructureRule(ruleName, new List<PhraseStructurePart>());
                    if (compoundRules != null && compoundRules[ruleName] != null && compoundRules[ruleName].Values<string>().Contains(ruleSub))
                    {
                        rule.Compound = true;
                    }
                    foreach (var phrase in ruleSub.Split(' '))
                    {
                        if (phrase.Contains("-"))
                        {
                            var parts = phrase.Split('-');
                            var p = parts[0];
                            var n = int.Parse(parts[1]);
                            rule.To.Add(new PhraseStructurePart(p, n));
                        }
                        else
                        {
                            rule.To.Add(new PhraseStructurePart(phrase));
                        }
                    }
                    rules.Add(rule);
                }
            }
            return rules;
        }

        public static LanguageGrammar FromJSON(string path)
        {
            return FromJSON(JObject.Parse(File.ReadAllText(path)));
        }

        public JToken ToJSON()
        {
            var o = new JObject();
            var rules = new JObject();

            var keys = this.Select(x => x.From).Distinct();
            foreach (var key in keys)
            {
                var jarray = new JArray();
                foreach (var rule in this.Where(x => x.From == key))
                {
                    jarray.Add(new JValue(rule.PartsToString()));
                }
                rules.Add(key, jarray);
            }

            o.Add("rules", rules);

            var compounds = this.Where(x => x.Compound);
            var compoundKeys = compounds.Select(x => x.From).Distinct().ToList();
            var compoundObj = new JObject();
            compoundKeys.ForEach(x => compoundObj.Add(x, new JArray()));

            foreach (var rule in compounds)
            {
                (compoundObj[rule.From] as JArray).Add(rule.PartsToString());
            }

            o.Add("compound", compoundObj);
            return o;
        }

        public static LanguageGrammar Generate()
        {
            var grammar = new LanguageGrammar();
            grammar.Add(new PhraseStructureRule("S", Globals.Random.Next(100) > 80
                ? new List<PhraseStructurePart>()
                {
                    new PhraseStructurePart("VP", 0),
                    new PhraseStructurePart("NP", 0),
                }
                : new List<PhraseStructurePart>()
                {
                    new PhraseStructurePart("NP", 0),
                    new PhraseStructurePart("VP", 0),
                }));
            grammar.Add(new PhraseStructureRule("S", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("S", 1),
                new PhraseStructurePart("VP", 0),
                new PhraseStructurePart("S", 2)
            }));

            grammar.Add(new PhraseStructureRule("NP", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("PRON", 0),
            }));
            grammar.Add(new PhraseStructureRule("NP", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("NN", 0),
            }));
            grammar.Add(new PhraseStructureRule("NP", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("DETM", 0),
                new PhraseStructurePart("NN", 0),
            }));
            grammar.Add(new PhraseStructureRule("NP", Globals.Random.Next(100) < 80
                ? new List<PhraseStructurePart>()
                {
                    new PhraseStructurePart("NP", 0),
                    new PhraseStructurePart("PP", 0),
                }
                : new List<PhraseStructurePart>()
                {
                    new PhraseStructurePart("PP", 0),
                    new PhraseStructurePart("NP", 0),
                }));
            grammar.Add(new PhraseStructureRule("NP", Globals.Random.Next(100) < 80
                ? new List<PhraseStructurePart>()
                {
                    new PhraseStructurePart("NP", 0),
                    new PhraseStructurePart("RC", 0),
                }
                : new List<PhraseStructurePart>()
                {
                    new PhraseStructurePart("RC", 0),
                    new PhraseStructurePart("NP", 0),
                }));
            grammar.Add(new PhraseStructureRule("NP", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("PS", 0),
                new PhraseStructurePart("NN", 0),
            }));

            grammar.Add(new PhraseStructureRule("VP", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("VERB", 0),
            }));
            grammar.Add(new PhraseStructureRule("VP", Globals.Random.Next(100) < 80
                ? new List<PhraseStructurePart>()
                {
                    new PhraseStructurePart("VERB", 0),
                    new PhraseStructurePart("NP", 0),
                }
                : new List<PhraseStructurePart>()
                {
                    new PhraseStructurePart("NP", 0),
                    new PhraseStructurePart("VERB", 0),
                }));
            grammar.Add(new PhraseStructureRule("VP", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("VERB", 0),
                new PhraseStructurePart("ADJC", 0),
            }));
            grammar.Add(new PhraseStructureRule("VP", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("VERB", 0),
                new PhraseStructurePart("PP", 0),
            }));
            grammar.Add(new PhraseStructureRule("VP", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("VERB", 0),
                new PhraseStructurePart("ADVB", 0),
            }));
            grammar.Add(new PhraseStructureRule("VP", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("VERB", 0),
                new PhraseStructurePart("PRON", 0),
            }));

            grammar.Add(new PhraseStructureRule("PP", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("PREP", 0),
                new PhraseStructurePart("NP", 0),
            }));

            grammar.Add(new PhraseStructureRule("RC", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("RLTV", 0),
                new PhraseStructurePart("VP", 0),
            }));

            grammar.Add(new PhraseStructureRule("NN", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("NOUN", 0),
            }));
            grammar.Add(new PhraseStructureRule("NN", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("ADJC", 0),
                new PhraseStructurePart("NN", 0),
            }, Globals.Random.Next(100) > 50));
            grammar.Add(new PhraseStructureRule("NN", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("CN", 0),
            }));

            grammar.Add(new PhraseStructureRule("CN", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("NOUN", 0),
                new PhraseStructurePart("CN", 0),
            }));
            grammar.Add(new PhraseStructureRule("CN", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("NOUN", 1),
                new PhraseStructurePart("NOUN", 2),
            }, Globals.Random.Next(100) > 50));

            grammar.Add(new PhraseStructureRule("PS", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("NOUN", 0),
            }));
            grammar.Add(new PhraseStructureRule("PS", new List<PhraseStructurePart>()
            {
                new PhraseStructurePart("PRON", 0),
            }));

            return grammar;
        }
    }

    public class PhraseStructurePart
    {
        public string Tag;
        public int Number;

        public PhraseStructurePart(string tag, int number)
        {
            Tag = tag;
            Number = number;
        }

        public PhraseStructurePart(string tag)
        {
            Tag = tag;
            Number = 0;
        }

        public override string ToString()
        {
            return Tag + (Number > 0 ? "-" + Number : "");
        }
    }

    public class PhraseStructureRule
    {
        public string From;
        public List<PhraseStructurePart> To;

        public bool Compound;

        public PhraseStructureRule(string from, List<PhraseStructurePart> to)
        {
            From = from;
            To = to;
        }

        public PhraseStructureRule(string from, List<PhraseStructurePart> to, bool compound) : this(from, to)
        {
            Compound = compound;
        }

        public string PartsToString()
        {
            return String.Join(" ", To.Select(x => x.ToString()));
        }
    }
}