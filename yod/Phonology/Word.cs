using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace yod.Phonology
{
    public class Word
    {
        readonly LanguagePhonology language;

        public int SyllableLength => Syllables.Count;
        public List<Syllable> Syllables;

        public Word(Word w)
        {
            language = w.language;
            Syllables = new List<Syllable>();
            w.Syllables.ForEach(x => Syllables.Add(new Syllable(x)));
        }

        public Word(LanguagePhonology language)
        {
            this.language = language;
            Generate();
        }

        public Word(LanguagePhonology language, int syllableLength)
        {
            this.language = language;
            Generate(syllableLength);
        }

        public void Generate()
        {
            var syllableLength = language.WordLengthMin + Globals.Random.Next(language.WordLengthMax - language.WordLengthMin);
            Generate(syllableLength);
        }

        public void Generate(int syllableLength)
        {
            Syllables = new List<Syllable>();
            for (var i = 0; i < syllableLength; i++)
            {
                var syl = new Syllable(language);
                if (Syllables.Count == 0)
                {
                    Syllables.Add(syl);
                }
                else
                {
                    //while (Syllables.Last().Phonemes.Last() == syl.Phonemes.First()) syl = new Syllable(language);
                    Syllables.Add(syl);
                }
            }

            ApplyStress();

            if (!IsValid())
            {
                Generate();
            }
        }

        // fixes stress placement and consecutive identical phonemes
        public void Fix()
        {
            Syllables.RemoveAll(x => x.Phonemes.Count == 0);
            ApplyGeminates();
            ApplyStress();
        }

        void ApplyGeminates()
        {
            for (var i = 0; i < SyllableLength - 1; i++)
            {
                if (Syllables[i].Phonemes.Last().Symbol == Syllables[i + 1].Phonemes.First().Symbol)
                {
                    if (Syllables[i].Coda.Count > 0)
                    {
                        Syllables[i].Coda.Clear();
                        Syllables[i + 1].Onset.First().Long = language.GeminateConsonants;
                    }
                    else
                    {
                        Syllables[i].Nucleus.RemoveAt(Syllables[i].Nucleus.Count - 1);
                        if (Syllables[i].Nucleus.Count == 0)
                        {
                            Syllables[i + 1].Onset = Syllables[i].Onset;
                            Syllables[i].Onset.Clear();
                        }
                        Syllables[i + 1].Nucleus.First().Long = language.LongVowels;
                    }

                    if (Syllables[i].Phonemes.Count == 0)
                    {
                        Syllables.RemoveAt(i);
                        i--;
                    }
                }
            }

            return;
        }

        void ApplyStress()
        {
            if (SyllableLength == 1) return;
            var stressIndex = 0;
            switch (language.StressLocation)
            {
                case StressLocation.Initial:
                    stressIndex = 0;
                    break;
                case StressLocation.Second:
                    stressIndex = 1;
                    break;
                case StressLocation.Penultimate:
                    stressIndex = SyllableLength - 2;
                    break;
                case StressLocation.Ultimate:
                    stressIndex = SyllableLength - 1;
                    break;
            }
            stressIndex = stressIndex < 0 ? 0 : stressIndex;
            stressIndex = stressIndex >= SyllableLength ? SyllableLength - 1 : stressIndex;

            for (var i = 0; i < SyllableLength; i++)
            {
                Syllables[i].Stressed = false;
            }
            Syllables[stressIndex].Stressed = true;
        }

        private bool IsValid()
        {
            var phonemes = new List<Phoneme>();
            Syllables.ForEach(x => phonemes.AddRange(x.Phonemes));
            for (var i = 0; i < phonemes.Count - 1; i++)
            {
                if (phonemes[i].Symbol == phonemes[i + 1].Symbol)
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            var s = "";
            Syllables.ForEach(x => s += x.ToString());
            return s;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var word = (Word) obj;
            if (word.SyllableLength != SyllableLength) return false;
            for (var i = 0; i < SyllableLength; i++)
            {
                if (!word.Syllables[i].Equals(Syllables[i])) return false;
            }
            return true;
        }
    }
}