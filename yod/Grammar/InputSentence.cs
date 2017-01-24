using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar
{
    public class InputSentence
    {
        public InputWord Object, Subject, Verb;
        public SentenceOrder Order;

        public InputSentence()
        {
            Order = SentenceOrder.SVO;
            //Order = SentenceOrder.SOV;
        }
    }

    public enum SentenceOrder
    {
        SVO,
        SOV,
        VSO,
        VOS,
        OSV,
        OVS
    }
}
