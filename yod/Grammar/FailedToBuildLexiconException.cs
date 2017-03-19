using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar
{
    public class FailedToBuildLexiconException : Exception
    {
        public FailedToBuildLexiconException()
        {
        }

        public FailedToBuildLexiconException(string message) : base(message)
        {
        }

        public FailedToBuildLexiconException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
