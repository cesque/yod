using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

using Newtonsoft.Json;

using yod;
using yod.Phonology;
using yod.Orthography;
using yod.Grammar;
using yod.Grammar.Structure;

namespace yodTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var tests = new Tests();

            tests.Run();
        }
    }
}
