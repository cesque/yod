using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class NounPhrase : GrammarPhrase
    {
        static readonly List<string> Rules = new List<string>()
        {
            "PRON", "NOUN", "DETM NOUN", "NP PP", "NP RC"
        };


        public string Rule;
        public List<GrammarPhrase> Parts;

        public NounPhrase(List<GrammarPhrase> parts)
        {
            Tag = "NP";
            IsTerminal = false;

            var list = parts.Select(x => x.Tag).ToList();
            foreach (var rule in Rules)
            {
                var q = rule.Split(' ').ToList();
                if (q.Count == list.Count && !q.Except(list).Any())
                {
                    Rule = rule;
                }
            }
            if (Rule == "") throw new Exception("Rule not found to match parts: " + String.Join(",", list));
            Parts = parts;
        }

        public override List<InputWord> Flatten()
        {
            var list = new List<InputWord>();

            if (Parts.Count == 1)
            {
                list.AddRange(Parts[0].Flatten());
            }
            else if (Rule == "DETM NOUN")
            {
                list.AddRange(Parts.Find(x => x.Tag == "DETM").Flatten());
                list.AddRange(Parts.Find(x => x.Tag == "NOUN").Flatten());
            } else if(Rule == "NP PP")
            {
                list.AddRange(Parts.Find(x => x.Tag == "NP").Flatten());
                list.AddRange(Parts.Find(x => x.Tag == "PP").Flatten());
            } else if(Rule == "NP RC")
            {
                list.AddRange(Parts.Find(x => x.Tag == "NP").Flatten());
                list.AddRange(Parts.Find(x => x.Tag == "RC").Flatten());
            }

            return list;
        }
    }
}
