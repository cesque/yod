﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace yod.Phonology
{
    public class VowelCollection : List<Vowel>
    {
        private static readonly List<Vowel> IPAVowels = new List<Vowel>
        {
            // todo: add more vowels
            new Vowel("i", Vowel.FrontToBack.Front, Vowel.OpenToClose.Close, Vowel.Rounded.Unrounded, 2),
            new Vowel("e", Vowel.FrontToBack.NearFront, Vowel.OpenToClose.CloseMid, Vowel.Rounded.Unrounded, 1),
            new Vowel("a", Vowel.FrontToBack.Central, Vowel.OpenToClose.Open, Vowel.Rounded.Unrounded, 0),
            new Vowel("u", Vowel.FrontToBack.Back, Vowel.OpenToClose.Close, Vowel.Rounded.Rounded, 2),
            new Vowel("o", Vowel.FrontToBack.Back, Vowel.OpenToClose.CloseMid, Vowel.Rounded.Rounded, 1)
        };

        public static VowelCollection AllVowels => new VowelCollection();

        public VowelCollection() : base(IPAVowels)
        {

        }

        public VowelCollection(List<Predicate<Vowel>> add)
        {
            add.ForEach(pred =>
            {
                AddRange(IPAVowels.Where(v => pred(v)));
            });
        }

        public Vowel GetRandom()
        {
            return this[Globals.Random.Next(Count)];
        }
    }
}
