using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using yod.Orthography;

namespace yod.Grammar
{
    public class Phrase
    {
        Dictionary<string, PartOfSpeech> posDict = new Dictionary<string, PartOfSpeech>
        {
            {"PRON", PartOfSpeech.Pronoun},
            {"NOUN", PartOfSpeech.Noun},
            {"VERB", PartOfSpeech.Verb},
            {"ADVB", PartOfSpeech.Adverb},
            {"ADJC", PartOfSpeech.Adjective},
            {"CONJ", PartOfSpeech.Conjunction},
            {"PREP", PartOfSpeech.Preposition},
            {"INTJ", PartOfSpeech.Interjection},
            {"DETM", PartOfSpeech.Determiner},
            {"RLTV", PartOfSpeech.Relativizer}
        };

        public readonly bool IsTerminal;
        public bool Compound;

        public Word Word
        {
            get
            {
                if (!IsTerminal) throw new FieldAccessException("Can't access Word of non-terminal phrase.");
                return _word;
            }
            set
            {
                if (!IsTerminal) throw new FieldAccessException("Can't access Word of non-terminal phrase.");
                _word = value;
            }
        }

        Word _word;

        public List<Phrase> Phrases
        {
            get
            {
                if (IsTerminal) throw new FieldAccessException("Can't access Phrases of terminal phrase.");
                return _phrases;
            }
            set
            {
                if (IsTerminal) throw new FieldAccessException("Can't access Phrases of terminal phrase.");
                _phrases = value;
            }
        }

        List<Phrase> _phrases;

        int Number;

        public Phrase(LanguageGrammar grammar, string inputPath) : this(grammar, JObject.Parse(File.ReadAllText(inputPath)))
        {
        }

        public string Tag;
        public PhraseStructureRule Rule;

        public Phrase(LanguageGrammar grammar, JToken jobj)
        {
            if (jobj.Parent == null) jobj = jobj.First;
            var o = (jobj as JProperty);
            var name = o.Name;
            var phrase = o.Value as JObject;

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

            if (phrase["word"] != null)
            {
                // is terminal
                IsTerminal = true;
                Word = new Word(
                    phrase.Value<string>("word"),
                    posDict[Tag],
                    phrase.Value<string>("tags")
                );
            }
            else
            {
                // is not terminal
                IsTerminal = false;
                Phrases = new List<Phrase>();
                var matchesRule = false;

                var matchingTagRules = grammar.Where(x => x.From == Tag);

                foreach (var rule in matchingTagRules)
                {
                    if (phrase.Children().Count() == rule.To.Count && phrase.Children().All(y => phrase.Children().Count(p => p == y) == rule.To.Count(p => p.Tag == (y as JProperty).Name)))
                    {
                        matchesRule = true;
                        Rule = rule;
                        break;
                    }
                }

                if (matchesRule == false) throw new ArgumentException("Couldn't create phrase " + Tag);
                if (!IsTerminal)
                {
                    phrase.Children().ToList().ForEach(x => { Phrases.Add(new Phrase(grammar, x)); });
                }
            }
        }

        public void Fill(Lexicon lexicon)
        {
            if (IsTerminal)
            {
                var word = lexicon.FirstOrDefault(w => w.English == Word.EnglishLemma && w.POS == Word.POS);
                if (word == null) throw new LexemeNotFoundException();
                Word.Fill(word.Lemma);
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
                var ruleParts = Rule.To;
                ruleParts.ForEach(phrase =>
                {
                    if (phrase.Number > 0)
                    {
                        var tag = phrase.Tag;
                        var num = phrase.Number;
                        list.AddRange(Phrases.Find(x => x.Tag == tag && x.Number == num).Flatten());
                    }
                    else
                    {
                        list.AddRange(Phrases.Find(x => x.Tag == phrase.Tag).Flatten());
                    }
                });
            }

            if (!IsTerminal && Rule.Compound)
            {
                return new List<Word>()
                {
                    list.Aggregate((prev, next) => Word.Merge(prev, next))
                };
            }
            else
            {
                return list;
            }
        }

        public void InflectAll(List<Inflection> inflections)
        {
            if (IsTerminal)
            {
                Word.Inflect(inflections);
            }
            else
            {
                Phrases.ForEach(x => x.InflectAll(inflections));
            }
        }

        public string ToString(LanguageOrthography orthography)
        {
            var flattened = Flatten();

            var line1 = "";
            var line2 = "/";
            var line3 = "";

            foreach (var word in flattened)
            {
                line1 += orthography.Orthographize(word.Inflected) + " ";
                line2 += word.Inflected.ToString() + " ";
                line3 += word.ToGlossString() + " ";
            }

            line2 = line2.Trim() + "/";

            return line1 + Environment.NewLine + line2 + Environment.NewLine + line3 + Environment.NewLine;
        }

        public override string ToString()
        {
            var flattened = Flatten();

            var line1 = "";
            var line2 = "/";

            foreach (var word in flattened)
            {
                line1 += word.Inflected.ToString() + " ";
                line2 += word.ToGlossString() + " ";
            }

            line2 = line2.Trim() + "/";

            return line1 + Environment.NewLine + line2 + Environment.NewLine;
        }
    }
}