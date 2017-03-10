using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace yod.Grammar
{
    public class LanguageGrammar : Dictionary<string, List<string>>
    {
        public LanguageGrammar()
        {
                
        }

        public static LanguageGrammar FromJSON(JToken jtoken)
        {
            LanguageGrammar rules = new LanguageGrammar();
            var o = jtoken as JObject;
            foreach (var v in o.Properties())
            {
                var ruleName = v.Name;
                var expandsTo = (v.Value as JArray).Values<string>().ToList();
                rules.Add(ruleName, expandsTo);
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
            foreach (var pair in this)
            {
                var ruleName = pair.Key;
                var expandsTo = pair.Value;

                var jarray = new JArray(expandsTo);

                o.Add(ruleName, jarray);
            }

            return o;
        }
    }
}
