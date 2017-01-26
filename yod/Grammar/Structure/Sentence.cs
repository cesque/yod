using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar.Structure
{
    public class Sentence : GrammarPhrase
    {
        static readonly List<string> Rules = new List<string>()
        {
            "NP VP",
            "S CONJ S"
        };


        public string Rule;
        public List<GrammarPhrase> Parts;

        public Sentence(List<GrammarPhrase> parts)
        {
            Tag = "S";
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
            Parts = parts;
        }

        public override List<InputWord> Flatten()
        {
            var list = new List<InputWord>();

            switch(Rule)
            {
                case "NP VP":
                    var s = Parts.Find(x => x.Tag == "NP");
                    var vp = (VerbPhrase)Parts.Find(x => x.Tag == "VP");
                    if (vp.Rule == "VERB NP")
                    {
                        var v = vp.Parts.Find(x => x.Tag == "VERB");
                        var o = vp.Parts.Find(x => x.Tag == "NP");

                        list.AddRange(s.Flatten());
                        list.AddRange(v.Flatten());
                        list.AddRange(o.Flatten());
                    }
                    else
                    {
                        list.AddRange(s.Flatten());
                        list.AddRange(vp.Flatten());
                    }
                    break;
                case "S CONJ S":
                    var sentences = Parts.Where(x => x.Tag == "S").ToList();
                    list.AddRange(sentences[0].Flatten());
                    list.AddRange(Parts.Find(x => x.Tag == "CONJ").Flatten());
                    list.AddRange(sentences[1].Flatten());
                    break;
            }

            return list;
        }
    }
}
