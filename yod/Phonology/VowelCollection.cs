using System;
using System.Collections.Generic;
using System.Linq;

namespace yod.Phonology
{
    public class VowelCollection : List<Vowel>
    {
        private static readonly List<Vowel> IPAVowels = new List<Vowel>
        {
            // todo: add more vowels
            new Vowel("i", Vowel.OpenToClose.Close, Vowel.FrontToBack.Front, Vowel.Rounded.Unrounded, sonority: 2),
            new Vowel("y", Vowel.OpenToClose.Close, Vowel.FrontToBack.Front, Vowel.Rounded.Rounded, sonority: 2),
            new Vowel("ɨ", Vowel.OpenToClose.Close, Vowel.FrontToBack.Central, Vowel.Rounded.Unrounded, sonority: 2),
            new Vowel("ʉ", Vowel.OpenToClose.Close, Vowel.FrontToBack.Central, Vowel.Rounded.Rounded, sonority: 2),
            new Vowel("ɯ", Vowel.OpenToClose.Close, Vowel.FrontToBack.Back, Vowel.Rounded.Unrounded, sonority: 2),
            new Vowel("u", Vowel.OpenToClose.Close, Vowel.FrontToBack.Back, Vowel.Rounded.Rounded, sonority: 2),
            new Vowel("ɪ", Vowel.OpenToClose.NearClose, Vowel.FrontToBack.NearFront, Vowel.Rounded.Unrounded, 2),
            new Vowel("ʏ", Vowel.OpenToClose.NearClose, Vowel.FrontToBack.NearFront, Vowel.Rounded.Rounded, 2),
            new Vowel("ɪ̈", Vowel.OpenToClose.NearClose, Vowel.FrontToBack.Central, Vowel.Rounded.Unrounded, 2),
            new Vowel("ʊ̈", Vowel.OpenToClose.NearClose, Vowel.FrontToBack.Central, Vowel.Rounded.Rounded, 2),
            new Vowel("ʊ", Vowel.OpenToClose.NearClose, Vowel.FrontToBack.NearBack, Vowel.Rounded.Rounded, 2),
            new Vowel("e", Vowel.OpenToClose.CloseMid, Vowel.FrontToBack.Front, Vowel.Rounded.Unrounded, 1),
            new Vowel("ø", Vowel.OpenToClose.CloseMid, Vowel.FrontToBack.Front, Vowel.Rounded.Rounded, 1),
            new Vowel("e", Vowel.OpenToClose.CloseMid, Vowel.FrontToBack.Front, Vowel.Rounded.Unrounded, 1),
            new Vowel("ø", Vowel.OpenToClose.CloseMid, Vowel.FrontToBack.Front, Vowel.Rounded.Rounded, 1),
            new Vowel("ɘ", Vowel.OpenToClose.CloseMid, Vowel.FrontToBack.Central, Vowel.Rounded.Unrounded, 1),
            new Vowel("ɵ", Vowel.OpenToClose.CloseMid, Vowel.FrontToBack.Central, Vowel.Rounded.Rounded, 1),
            new Vowel("ɤ", Vowel.OpenToClose.CloseMid, Vowel.FrontToBack.Back, Vowel.Rounded.Unrounded, 1),
            new Vowel("o", Vowel.OpenToClose.CloseMid, Vowel.FrontToBack.Back, Vowel.Rounded.Rounded, 1),
            new Vowel("ə", Vowel.OpenToClose.Mid, Vowel.FrontToBack.Central, Vowel.Rounded.Unrounded, 1),
            new Vowel("ɤ̞", Vowel.OpenToClose.Mid, Vowel.FrontToBack.Back, Vowel.Rounded.Unrounded, 1),
            new Vowel("o̞", Vowel.OpenToClose.Mid, Vowel.FrontToBack.Back, Vowel.Rounded.Rounded, 1),
            new Vowel("ɛ", Vowel.OpenToClose.OpenMid, Vowel.FrontToBack.Front, Vowel.Rounded.Unrounded, 0),
            new Vowel("œ", Vowel.OpenToClose.OpenMid, Vowel.FrontToBack.Front, Vowel.Rounded.Rounded, 0),
            new Vowel("ɜ", Vowel.OpenToClose.OpenMid, Vowel.FrontToBack.Central, Vowel.Rounded.Unrounded, 0),
            new Vowel("ɞ", Vowel.OpenToClose.OpenMid, Vowel.FrontToBack.Central, Vowel.Rounded.Rounded, 0),
            new Vowel("ʌ", Vowel.OpenToClose.OpenMid, Vowel.FrontToBack.Back, Vowel.Rounded.Unrounded, 0),
            new Vowel("ɔ", Vowel.OpenToClose.OpenMid, Vowel.FrontToBack.Back, Vowel.Rounded.Rounded, 0),
            new Vowel("æ", Vowel.OpenToClose.NearOpen, Vowel.FrontToBack.Front, Vowel.Rounded.Unrounded, 0),
            new Vowel("ɐ", Vowel.OpenToClose.NearOpen, Vowel.FrontToBack.Central, Vowel.Rounded.Unrounded, 0),
            new Vowel("a", Vowel.OpenToClose.Open, Vowel.FrontToBack.Front, Vowel.Rounded.Unrounded, 0),
            new Vowel("ɶ", Vowel.OpenToClose.Open, Vowel.FrontToBack.Front, Vowel.Rounded.Rounded, 0),
            new Vowel("ä", Vowel.OpenToClose.Open, Vowel.FrontToBack.Central, Vowel.Rounded.Unrounded, 0),
            new Vowel("ɑ", Vowel.OpenToClose.Open, Vowel.FrontToBack.Back, Vowel.Rounded.Unrounded, 0),
            new Vowel("ɒ", Vowel.OpenToClose.Open, Vowel.FrontToBack.Back, Vowel.Rounded.Rounded, 0),
        };

        public static VowelCollection AllVowels => new VowelCollection();

        public static VowelCollection EnglishVowels => new VowelCollection(new List<Predicate<Vowel>> {x => "iɪuʊeɜəɔæʌɑɒ".Contains(x.Symbol)});

        public VowelCollection() : base(EnglishVowels)
        {
        }

        public VowelCollection(List<Predicate<Vowel>> add)
        {
            add.ForEach(pred => { AddRange(IPAVowels.Where(v => pred(v))); });
        }

        public Vowel GetRandom()
        {
            return this[Globals.Random.Next(Count)];
        }
    }
}