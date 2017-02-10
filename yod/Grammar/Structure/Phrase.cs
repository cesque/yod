using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace yod.Grammar.Structure
{
    public class Phrase
    {
        List<string> terminals = "PRON,NOUN,VERB,ADVB,ADJC,CONJ,INTJ,DETM,PREP,RLTV".Split(',').ToList();
        Dictionary<string, PartOfSpeech> posDict = new Dictionary<string, PartOfSpeech>()
            {
                {"PRON", PartOfSpeech.PRONOUN},
                {"NOUN", PartOfSpeech.NOUN },
                {"VERB", PartOfSpeech.VERB },
                {"ADVB", PartOfSpeech.ADVERB },
                {"ADJC", PartOfSpeech.ADJECTIVE },
                {"CONJ", PartOfSpeech.CONJUNCTION },
                {"PREP", PartOfSpeech.PREPOSITION },
                {"INTJ", PartOfSpeech.INTERJECTION },
                {"DETM", PartOfSpeech.DETERMINER },
                {"RLTV", PartOfSpeech.RELATIVIZER }
            };
        Dictionary<string, List<string>> rules;

        public readonly bool IsTerminal;

        public Word Word
        {
            get
            {
                if (!IsTerminal) throw new Exception("Can't access Word of non-terminal phrase.");
                return _word;
            }
            set
            {
                if (!IsTerminal) throw new Exception("Can't access Word of non-terminal phrase.");
                _word = value;
            }
        }
        Word _word;

        public List<Phrase> Phrases
        {
            get
            {
                if (IsTerminal) throw new Exception("Can't access Phrases of terminal phrase.");
                return _phrases;
            }
            set
            {
                if (IsTerminal) throw new Exception("Can't access Phrases of terminal phrase.");
                _phrases = value;
            }
        }
        List<Phrase> _phrases;

        int Number = 0;

        public Phrase(string rulesPath, string inputPath)
            : this(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText(rulesPath)),
              JObject.Parse(File.ReadAllText(inputPath)))
        { }

        public string Tag;
        public string Rule;

        public Phrase(Dictionary<string, List<string>> rulesDict, JToken jobj)
        {
            if (jobj.Parent == null)
            {
                // seems to be a weird quirk of parsing JSON, this accesses the first element of json object
                jobj = jobj.First.First;
            }
            var name = ((JProperty)jobj.Parent).Name;
            if (name.Contains("-"))
            {
                var parts = name.Split('-');
                Tag = parts[0];
                Number = int.Parse(parts[1]);
            }
            else
            {
                Tag = name;
            }

            rules = rulesDict;
            if (jobj["word"] != null)
            {
                // is terminal
                IsTerminal = true;
                Word = new Word(
                   jobj.Value<string>("word"),
                   posDict[Tag],
                   jobj.Value<string>("tags")
                );
            }
            else
            {
                // is not terminal
                IsTerminal = false;
                Phrases = new List<Phrase>();
                var subphrases = ((JObject)jobj).Properties().Select(x => x.Name).ToList();
                var matchesRule = false;
                foreach (var rule in rules[Tag])
                {
                    var parts = rule.Split(' ').ToList();

                    if (subphrases.Count == parts.Count && subphrases.All(y => subphrases.Count(p => p == y) == parts.Count(p => p == y)))
                    {
                        matchesRule = true;
                        Rule = rule;
                        break;
                    }
                }

                if (matchesRule == false) throw new Exception("Couldn't create phrase " + Tag);
                subphrases.ForEach(x =>
                {
                    Phrases.Add(new Phrase(rules, jobj[x]));
                });
            }
        }

        public void Fill(Lexicon lexicon)
        {
            if (IsTerminal)
            {
                var word = lexicon.First(w => w.English == Word.EnglishLemma && w.POS == Word.POS);
                Word.Phonemes = word.Lemma;
            }
            else
            {
                Phrases.ForEach(x => x.Fill(lexicon));
            }
        }

        public List<Word> Flatten()
        {
            var list = new List<Word>();
            if (IsTerminal)
            {
                list.Add(Word);
            }
            else
            {
                var ruleParts = Rule.Split(' ').ToList();
                ruleParts.ForEach(phrase =>
                {
                    if(phrase.Contains('-'))
                    {
                        var parts = phrase.Split('-');
                        var tag = parts[0];
                        var num = int.Parse(parts[1]);
                        list.AddRange(Phrases.Find(x => x.Tag == tag && x.Number == num).Flatten());
                    } else
                    {
                        list.AddRange(Phrases.Find(x => x.Tag == phrase).Flatten());
                    }
                });
            }
            return list;
        }
    }
}
