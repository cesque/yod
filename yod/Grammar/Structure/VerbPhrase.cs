using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class VerbPhrase : GrammarPhrase
    {
        static readonly List<string> Rules = new List<string>()
        {
            "VERB", "VERB NP", "VERB ADJC", "VERB PP", "VERB ADVB"
        };


        public string Rule;
        public List<GrammarPhrase> Parts;

        public VerbPhrase(List<GrammarPhrase> parts)
        {
            Tag = "VP";
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
            else if (Rule == "VERB NP")
            {
                list.AddRange(Parts.Find(x => x.Tag == "VERB").Flatten());
                list.AddRange(Parts.Find(x => x.Tag == "NP").Flatten());
            } else if(Rule == "VERB ADJC")
            {
                list.AddRange(Parts.Find(x => x.Tag == "VERB").Flatten());
                list.AddRange(Parts.Find(x => x.Tag == "ADJC").Flatten());
            } else if(Rule == "VERB PP")
            {
                list.AddRange(Parts.Find(x => x.Tag == "VERB").Flatten());
                list.AddRange(Parts.Find(x => x.Tag == "PP").Flatten());
            } else if(Rule == "VERB ADVB")
            {
                list.AddRange(Parts.Find(x => x.Tag == "VERB").Flatten());
                list.AddRange(Parts.Find(x => x.Tag == "ADVB").Flatten());
            }

            return list;
        }
    }
}
