using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod
{
    public class VowelCollection : List<Vowel>
    {
        private static readonly List<Vowel> IPAVowels = new List<Vowel>
        {
            new Vowel("i", Vowel.FrontToBack.FRONT, Vowel.OpenToClose.CLOSE, Vowel.Rounded.UNROUNDED, 2),
            new Vowel("e", Vowel.FrontToBack.NEARFRONT, Vowel.OpenToClose.CLOSEMID, Vowel.Rounded.UNROUNDED, 1),
            new Vowel("a", Vowel.FrontToBack.CENTRAL, Vowel.OpenToClose.OPEN, Vowel.Rounded.UNROUNDED, 0),
            new Vowel("u", Vowel.FrontToBack.BACK, Vowel.OpenToClose.CLOSE, Vowel.Rounded.ROUNDED, 2),
            new Vowel("o", Vowel.FrontToBack.BACK, Vowel.OpenToClose.CLOSEMID, Vowel.Rounded.ROUNDED, 1),
        };

        public static VowelCollection AllVowels
        {
            get
            {
                return new VowelCollection();
            }
        }

        public VowelCollection() : base(IPAVowels)
        {

        }

        public VowelCollection(List<Predicate<Vowel>> add)
        {
            add.ForEach(pred =>
            {
                this.AddRange(IPAVowels.Where(v => pred(v)));
            });
        }

        public Vowel GetRandom()
        {
            return this[Globals.Random.Next(this.Count)];
        }
    }
}
