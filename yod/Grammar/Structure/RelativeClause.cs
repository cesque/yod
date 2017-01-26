using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class RelativeClause : GrammarPhrase
    {
        static readonly List<string> Rules = new List<string>()
        {
            "RLTV VP"
        };


        public string Rule;
        public List<GrammarPhrase> Parts;

        public RelativeClause(List<GrammarPhrase> parts)
        {
            Tag = "RC";
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
            else if (Rule == "RLTV VP")
            {
                list.AddRange(Parts.Find(x => x.Tag == "RLTV").Flatten());
                list.AddRange(Parts.Find(x => x.Tag == "VP").Flatten());
            }

            return list;
        }
    }
}
