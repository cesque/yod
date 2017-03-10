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
            Fill(words, null, null, phonology);
        }

        public void Fill(Dictionary<PartOfSpeech, List<string>> words,
            Dictionary<PartOfSpeech, List<string>> commonWords,
            Dictionary<string, Tuple<PartOfSpeech, string>> relatedWords,
            LanguagePhonology phonology)
        {
            commonWords = commonWords ?? new Dictionary<PartOfSpeech, List<string>>();
            relatedWords = relatedWords ?? new Dictionary<string, Tuple<PartOfSpeech, string>>();

            var baseWords = new Dictionary<string, Phonology.Word>();
            foreach (var kp in relatedWords)
            {
                var word = kp.Key;
                var pos = kp.Value.Item1;
                var group = kp.Value.Item2;

                var common = commonWords.ContainsKey(pos) && commonWords[pos].Contains(word);

                var baseWord = common
                    ? new Phonology.Word(phonology, syllableLength: phonology.WordLengthMin)
                    : new Phonology.Word(phonology);

                if (!baseWords.ContainsKey(group)) baseWords.Add(group, baseWord);
            }

            foreach (var pair in words)
            {
                foreach (var word in pair.Value)
                {
                    var loops = 0;
                    var w = GenerateWordToAdd(word, pair.Key, commonWords, relatedWords, baseWords, phonology);
                    while (Lexemes.Exists(x => x.Lemma.Equals(w)))
                    {
                        loops++;
                        if (loops < 50)
                        {
                            w = GenerateWordToAdd(word, pair.Key, commonWords, relatedWords, baseWords, phonology);
                            Console.WriteLine(w.ToString());
                        }
                        else
                        {       
                            // happens when phonemes + syllable structures + word length are all too restrictive 
                            throw new FailedToBuildLexiconException("Couldn't generate unique words for given language.");
                        }

                    }
                    Add(word, w, pair.Key);
                }
            }
        }

        Phonology.Word GenerateWordToAdd(string word, PartOfSpeech pos,
            Dictionary<PartOfSpeech, List<string>> commonWords,
            Dictionary<string, Tuple<PartOfSpeech, string>> relatedWords,
            Dictionary<string, Phonology.Word> baseWords,
            LanguagePhonology phonology)
        {
            if (relatedWords.ContainsKey(word) && relatedWords[word].Item1 == pos)
            {
                var group = relatedWords[word].Item2;
                var w = new Phonology.Word(baseWords[group]);
                w.Syllables.Last().Morph();
                return w;
            }
            else if (commonWords.ContainsKey(pos) && commonWords[pos].Contains(word))
            {
                var length = Globals.Random.Next(phonology.WordLengthMin, Math.Min(phonology.WordLengthMax, phonology.WordLengthMin + 1));
                return new Phonology.Word(phonology, syllableLength: length);
            }
            else
            {
                return new Phonology.Word(phonology);
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

            // 
            var relatedwords = (JObject) jobj.Value<JToken>("related");
            var relatedDict = new Dictionary<string, Tuple<PartOfSpeech, string>>();
            foreach (var group in relatedwords)
            {
                foreach (var posString in (JObject) group.Value)
                {
                    var pos = posDict[posString.Key];
                    var list = posString.Value.Values<string>().ToList();
                    list.ForEach(x => { relatedDict.Add(x, new Tuple<PartOfSpeech, string>(pos, group.Key)); });
                }
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

            Fill(words, commonDict, relatedDict, phonology);
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