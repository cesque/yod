using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            foreach (var v in rulesObj.Properties())
            {
                var ruleName = v.Name;
                var expandsTo = (v.Value as JArray).Values<string>().ToList();              
                foreach (var ruleSub in expandsTo)
                {
                    var rule = new PhraseStructureRule(ruleName, new List<PhraseStructurePart>());
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
            return o;
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

        public PhraseStructureRule(string from, List<PhraseStructurePart> to)
        {
            From = from;
            To = to;
        }

        public string PartsToString()
        {
            return String.Join(" ", To.Select(x => x.ToString()));
        }
    }
}