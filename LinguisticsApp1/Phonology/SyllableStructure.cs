using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Phonology
{
    public class SyllableStructure
    {
        public List<SyllableStructureOption> OnsetStructure;
        public List<SyllableStructureOption> NucleusStructure;
        public List<SyllableStructureOption> CodaStructure;

        public SyllableStructure()
        {

        }

        public int GetOnsetLength()
        {
            var dict = new Dictionary<int, float>();
            OnsetStructure.ForEach(x => dict.Add(x.Length, x.Weight));
            return Globals.WeightedRandom<int>(dict);
        }

        public int GetNucleusLength()
        {
            var dict = new Dictionary<int, float>();
            NucleusStructure.ForEach(x => dict.Add(x.Length, x.Weight));
            return Globals.WeightedRandom<int>(dict);
        }

        public int GetCodaLength()
        {
            var dict = new Dictionary<int, float>();
            CodaStructure.ForEach(x => dict.Add(x.Length, x.Weight));
            return Globals.WeightedRandom<int>(dict);
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
    }
}
