using System;
using System.Collections.Generic;
using System.Linq;

namespace yod.Phonology
{
    public class Syllable
    {
        LanguagePhonology language;
        SyllableStructure structure;

        public List<Phoneme> Onset, Nucleus, Coda;

        public List<Phoneme> Phonemes => new List<Phoneme>()
            .Concat(Onset)
            .Concat(Nucleus)
            .Concat(Coda)
            .ToList();

        public bool Stressed = false;

        public Syllable(Syllable syllable)
        {
            language = syllable.language;
            structure = syllable.structure;
            Onset = new List<Phoneme>();
            Nucleus = new List<Phoneme>();
            Coda = new List<Phoneme>();
            Stressed = syllable.Stressed;
            syllable.Onset.ForEach(x =>
            {
                Onset.Add(x.Type == Phoneme.PhonemeType.Consonant
                    ? new Consonant((Consonant) x)
                    : (Phoneme) new Vowel((Vowel) x));
            });
            syllable.Nucleus.ForEach(x =>
            {
                Nucleus.Add(x.Type == Phoneme.PhonemeType.Consonant
                    ? new Consonant((Consonant) x)
                    : (Phoneme) new Vowel((Vowel) x));
            });
            syllable.Coda.ForEach(x =>
            {
                Coda.Add(x.Type == Phoneme.PhonemeType.Consonant
                    ? new Consonant((Consonant) x)
                    : (Phoneme) new Vowel((Vowel) x));
            });
        }

        public Syllable(LanguagePhonology l)
        {
            language = l;
            structure = l.SyllableStructure;
            Generate();
        }

        public void Generate()
        {
            Onset = new List<Phoneme>();
            Nucleus = new List<Phoneme>();
            Coda = new List<Phoneme>();

            Onset.AddRange(GetOnset());
            Nucleus.AddRange(GetNucleus());
            Coda.AddRange(GetCoda());

            if(!IsValid()) Generate();
        }

        private bool IsValid()
        {
            var plocal = Phonemes;
            for (var i = 0; i < plocal.Count - 1; i++)
            {
                if (plocal[i].Symbol == plocal[i + 1].Symbol) return false;
            }
            return true;
        }

        public override string ToString()
        {
            var s = "";
            if (Stressed) s += "ˈ";
            Phonemes.ForEach(x => s += x.ToString());
            return s;
        }

        private List<Phoneme> GetOnset(int length)
        {
            var list = new List<Phoneme>();

            var minSonority = 2 + (length - 1);
            var maxSonority = 9;

            for (var i = 0; i < length; i++)
            {
                var cons = language.Phonemes.Consonants.GetRandomInSonorityRange(minSonority, maxSonority);
                if (cons == null) break;
                minSonority--;
                maxSonority = cons.Sonority - 1;
                list.Add(cons);
            }

            return list;
        }

        private List<Phoneme> GetOnset()
        {
            return GetOnset(structure.GetOnsetLength());
        }

        private List<Phoneme> GetNucleus(int length)
        {
            var list = new List<Phoneme>();

            for (var i = 0; i < length; i++)
            {
                var v = language.Phonemes.Vowels.GetRandom();
                if (list.Count > 0) while (list.Last() == v) v = language.Phonemes.Vowels.GetRandom();
                list.Add(v);
            }

            return list;
        }

        private List<Phoneme> GetNucleus()
        {
            return GetNucleus(structure.GetNucleusLength());
        }

        private List<Phoneme> GetCoda(int length)
        {
            var list = new List<Phoneme>();

            var minSonority = 2;
            var maxSonority = 9 - (length - 1);

            for (var i = 0; i < length; i++)
            {
                var cons = language.Phonemes.Consonants.GetRandomInSonorityRange(minSonority, maxSonority);
                if (cons == null) break;
                maxSonority++;
                minSonority = cons.Sonority + 1;
                list.Add(cons);
            }

            return list;
        }

        private List<Phoneme> GetCoda()
        {
            return GetOnset(structure.GetCodaLength());
        }

        public void Morph()
        {
            var dict = new Dictionary<Action, float>
            {
                {MorphNucleus, 33f},
            };

            var onsetMax = structure.OnsetStructure.Max(x => x.Length);
            var codaMax = structure.CodaStructure.Max(x => x.Length);

            if (onsetMax > 0) dict.Add(MorphOnset, 33f);
            if (codaMax > 0) dict.Add(MorphCoda, 33f);

            // break the rules of syllable structure a little to prevent infinite
            // loops when trying to morph monosyllabic, single vowel words
            if (onsetMax == 0 && codaMax == 0 && Globals.Random.Next(100) > 50)
            {
                if (Globals.Random.Next(100) > 50)
                {
                    Coda = GetCoda(1);
                }
                else
                {
                    Onset = GetOnset(1);
                }
            }
            else
            {
                var morph = Globals.WeightedRandom(dict);
                morph();
            }
        }

        public void MorphOnset()
        {
            Onset = GetOnset();
        }

        public void MorphNucleus()
        {
            Nucleus = GetNucleus();
        }

        public void MorphCoda()
        {
            Coda = GetCoda();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var syllable = (Syllable) obj;

            if (Onset.Count != syllable.Onset.Count) return false;
            for (var i = 0; i < Onset.Count; i++)
            {
                if (!Onset[i].Equals(syllable.Onset[i])) return false;
            }
            if (Nucleus.Count != syllable.Nucleus.Count) return false;

            for (var i = 0; i < Nucleus.Count; i++)
            {
                if (!Nucleus[i].Equals(syllable.Nucleus[i])) return false;
            }

            if (Coda.Count != syllable.Coda.Count) return false;
            for (var i = 0; i < Coda.Count; i++)
            {
                if (!Coda[i].Equals(syllable.Coda[i])) return false;
            }

            if (Stressed != syllable.Stressed) return false;

            return true;
        }
    }
}