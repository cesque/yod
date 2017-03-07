using System.Collections.Generic;

namespace yod.Phonology
{
    public class SyllableStructure
    {
        public List<SyllableStructureOption> OnsetStructure;
        public List<SyllableStructureOption> NucleusStructure;
        public List<SyllableStructureOption> CodaStructure;

        public SyllableStructure(List<SyllableStructureOption> onsetStructure, List<SyllableStructureOption> nucleusStructure, List<SyllableStructureOption> codaStructure)
        {
            OnsetStructure = onsetStructure;
            NucleusStructure = nucleusStructure;
            CodaStructure = codaStructure;
        }

        public int GetOnsetLength()
        {
            var dict = new Dictionary<int, float>();
            OnsetStructure.ForEach(x => dict.Add(x.Length, x.Weight));
            return Globals.WeightedRandom(dict);
        }

        public int GetNucleusLength()
        {
            var dict = new Dictionary<int, float>();
            NucleusStructure.ForEach(x => dict.Add(x.Length, x.Weight));
            return Globals.WeightedRandom(dict);
        }

        public int GetCodaLength()
        {
            var dict = new Dictionary<int, float>();
            CodaStructure.ForEach(x => dict.Add(x.Length, x.Weight));
            return Globals.WeightedRandom(dict);
        }

        public class SyllableStructureOption
        {
            public int Length;
            public float Weight;

            public SyllableStructureOption(int length, float weight)
            {
                Length = length;
                Weight = weight;
            }
        }

        public static SyllableStructure Generate()
        {
            var onset = new List<SyllableStructureOption>();
            var maxOnset = Globals.WeightedRandom(new Dictionary<int, float>
            {
                {0, 10f},
                {1, 45f},
                {2, 45f}
            });
            for (var i = 0; i <= maxOnset; i++)
            {
                onset.Add(new SyllableStructureOption(i, 10 + Globals.Random.Next(i == 0 ? 35 : 90)));
            }

            var nucleus = new List<SyllableStructureOption>();
            var maxNucleus = Globals.WeightedRandom(new Dictionary<int, float>
            {
                {1, 66.6f},
                {2, 33.3f}
            });
            for (var i = 1; i <= maxNucleus; i++)
            {
                nucleus.Add(new SyllableStructureOption(i, 10 + Globals.Random.Next(90)));
            }

            var coda = new List<SyllableStructureOption>();
            var maxCoda = Globals.WeightedRandom(new Dictionary<int, float>
            {
                {0, 12f},
                {1, 32f},
                {2, 24f},
                {3, 12f}
            });
            for (var i = 0; i <= maxCoda; i++)
            {
                coda.Add(new SyllableStructureOption(i, 10 + Globals.Random.Next(90)));
            }

            return new SyllableStructure(onset, nucleus, coda);
        }
    }
}