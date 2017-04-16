using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

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

            public JToken ToJSON()
            {
                var o = new JObject();
                o.Add(Length.ToString(), new JValue(Weight));
                return o;
            }

            public static SyllableStructureOption FromJSON(JToken jtoken)
            {
                var o = (jtoken as JObject).First as JProperty;
                return new SyllableStructureOption(int.Parse(o.Name), (int)o.Value);
            }
        }

        public static SyllableStructure Generate()
        {
            var onset = new List<SyllableStructureOption>();
            var maxOnset = Globals.WeightedRandom(new Dictionary<int, float>
            {
                {0, 5f},
                {1, 50f},
                {2, 45f}
            });
            for (var i = Math.Min(Globals.Random.Next(100) > 50 ? 1 : 0, maxOnset); i <= maxOnset; i++)
            {
                onset.Add(new SyllableStructureOption(i, 10 + Globals.Random.Next(i == 0 ? 35 : 40)));
            }

            var nucleus = new List<SyllableStructureOption>();
            var maxNucleus = Globals.WeightedRandom(new Dictionary<int, float>
            {
                {1, 66.6f},
                {2, 33.3f}
            });
            for (var i = 1; i <= maxNucleus; i++)
            {
                nucleus.Add(new SyllableStructureOption(i, 10 + Globals.Random.Next(40)));
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
                coda.Add(new SyllableStructureOption(i, 10 + Globals.Random.Next(40)));
            }

            if (maxOnset == 0 && maxCoda == 0)
            {
                return Generate();
            }
            else
            {
                return new SyllableStructure(onset, nucleus, coda);
            }
        }

        public JToken ToJSON()
        {
            return new JObject
            {
                {"onset", new JArray(OnsetStructure.Select(x => x.ToJSON()))},
                {"nucleus", new JArray(NucleusStructure.Select(x => x.ToJSON()))},
                {"coda", new JArray(CodaStructure.Select(x => x.ToJSON()))}
            };
        }

        public static SyllableStructure FromJSON(JToken jToken)
        {
            var o = jToken as JObject;
            var structure = new SyllableStructure(new List<SyllableStructureOption>(), new List<SyllableStructureOption>(), new List<SyllableStructureOption>());
            (o["onset"] as JArray).ToList().ForEach(x => structure.OnsetStructure.Add(SyllableStructureOption.FromJSON(x)));
            (o["nucleus"] as JArray).ToList().ForEach(x => structure.NucleusStructure.Add(SyllableStructureOption.FromJSON(x)));
            (o["coda"] as JArray).ToList().ForEach(x => structure.CodaStructure.Add(SyllableStructureOption.FromJSON(x)));
            return structure;
        }

        public override string ToString()
        {
            var s = "";
            var minOnset = this.OnsetStructure.Min(option => option.Length);
            var maxOnset = this.OnsetStructure.Max(option => option.Length);
            for (var i = 0; i<minOnset; i++) s += "C";
            for (var i = 0; i < maxOnset - minOnset; i++) s += "(C)";

            var minNucleus = this.NucleusStructure.Min(option => option.Length);
            var maxNucleus = this.NucleusStructure.Max(option => option.Length);
            for (var i = 0; i < minNucleus; i++) s += "V";
            for (var i = 0; i < maxNucleus - minNucleus; i++) s += "(V)";

            var minCoda = this.CodaStructure.Min(option => option.Length);
            var maxCoda = this.CodaStructure.Max(option => option.Length);
            for (var i = 0; i < minCoda; i++) s += "C";
            for (var i = 0; i < maxCoda - minCoda; i++) s += "(C)";

            return s;
        }
    }
}