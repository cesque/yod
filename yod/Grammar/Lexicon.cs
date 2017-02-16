using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using yod.Orthography;
using yod.Phonology;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace yod.Grammar
{
    public class Lexicon : IEnumerable<Lexeme>
    {
        private List<Lexeme> Lexemes;

        public Lexicon()
        {
            Lexemes = new List<Lexeme>();
        }

        public Lexeme this[string englishLemma, PartOfSpeech pos]
        {
            get { return Lexemes.Find(x => x.English == englishLemma && x.POS == pos); }
        }

        public void Add(Lexeme lexeme)
        {
            Lexemes.Add(lexeme);
        }

        public void Add(string english, Phonology.Word lemma, PartOfSpeech pos)
        {
            Add(new Lexeme(english, lemma, pos));
        }

        public bool Contains(string english)
        {
            return Lexemes.Exists(x => x.English == english);
        }

        public IEnumerator<Lexeme> GetEnumerator()
        {
            return Lexemes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Fill(Dictionary<PartOfSpeech, List<string>> words, LanguagePhonology phonology)
        {
            Fill(words, new Dictionary<PartOfSpeech, List<string>>(), phonology);
        }

        public void Fill(Dictionary<PartOfSpeech, List<string>> words,
            Dictionary<PartOfSpeech, List<string>> commonWords,
            LanguagePhonology phonology)
        {
            foreach (var pair in words)
            {
                foreach (var word in pair.Value)
                {
                    if (commonWords.ContainsKey(pair.Key) && commonWords[pair.Key].Contains(word))
                    {
                        Add(word, new Phonology.Word(phonology, syllableLength: phonology.WordLengthMin), pair.Key);
                    }
                    else
                    {
                        Add(word, new Phonology.Word(phonology), pair.Key);
                    }
                }
            }
        }

        public void Fill(string filepath, LanguagePhonology phonology)
        {
            var posDict = new Dictionary<string, PartOfSpeech>
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

            var words = new Dictionary<PartOfSpeech, List<String>>();
            // todo: make list of similar words

            var jobj = JObject.Parse(File.ReadAllText(filepath));
            var partsofspeech = (JObject) jobj.Value<JToken>("words");

            // a bit hacky...
            var commonwords = (JObject) jobj.Value<JToken>("common");
            var commonDict = new Dictionary<PartOfSpeech, List<string>>();
            posDict.Values.ToList().ForEach(x => commonDict.Add(x, new List<string>()));
            foreach (var posString in commonwords)
            {
                var pos = posDict[posString.Key];
                var list = posString.Value.Values<string>().ToList();
                list.ForEach(x => { commonDict[pos].Add(x); });
            }

            foreach (var posString in partsofspeech)
            {
                var pos = posDict[posString.Key];
                var wordList = posString.Value;
                wordList.Values<string>().ToList().ForEach(w =>
                {
                    if (!words.ContainsKey(pos)) words.Add(pos, new List<string>());
                    words[pos].Add(w);
                });
            }

            Fill(words, commonDict, phonology);
        }

        public override string ToString()
        {
            var posDict = new Dictionary<PartOfSpeech, string>
            {
                {PartOfSpeech.Noun, "n"},
                {PartOfSpeech.Verb, "v"},
                {PartOfSpeech.Pronoun, "pron"},
                {PartOfSpeech.Adverb, "adv"},
                {PartOfSpeech.Adjective, "adj"},
                {PartOfSpeech.Preposition, "prep"},
                {PartOfSpeech.Conjunction, "conj"},
                {PartOfSpeech.Interjection, "intj"},
                {PartOfSpeech.Determiner, "art"},
                {PartOfSpeech.Relativizer, "rel"}
            };

            var s = "";
            var maxEnglish = this.Max(x => x.English.Length);
            var maxIpa = this.Max(x => x.Lemma.ToString().Length);
            foreach (var w in this)
            {
                var lDiff = w.Lemma.ToString().Length - Globals.StripTies(w.Lemma.ToString()).Length;
                s += (w.Lemma + " (" + posDict[w.POS] + ")").PadRight(maxIpa + lDiff + 8);
                s += ": ";
                s += w.English;
                s += Environment.NewLine;
            }

            return s;
        }

        public string ToString(LanguageOrthography orthography)
        {
            var posDict = new Dictionary<PartOfSpeech, string>
            {
                {PartOfSpeech.Noun, "n"},
                {PartOfSpeech.Verb, "v"},
                {PartOfSpeech.Pronoun, "pron"},
                {PartOfSpeech.Adverb, "adv"},
                {PartOfSpeech.Adjective, "adj"},
                {PartOfSpeech.Preposition, "prep"},
                {PartOfSpeech.Conjunction, "conj"},
                {PartOfSpeech.Interjection, "intj"},
                {PartOfSpeech.Determiner, "art"},
                {PartOfSpeech.Relativizer, "rel"}
            };

            var s = "";
            var maxEnglish = this.Max(x => x.English.Length);
            var maxIpa = this.Max(x => x.Lemma.ToString().Length);
            var maxOrth = this.Max(x => orthography.Orthographize(x.Lemma).Length);
            foreach (var w in this)
            {
                var lDiff = w.Lemma.ToString().Length - Globals.StripTies(w.Lemma.ToString()).Length;
                s +=
                    (orthography.Orthographize(w.Lemma) + " /" + w.Lemma + "/ (" + posDict[w.POS] + ")").PadRight(
                        maxIpa + maxOrth + lDiff + 8);
                s += ": ";
                s += w.English;
                s += Environment.NewLine;
            }

            return s;
        }
    }
}