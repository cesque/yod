using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace yod.Grammar
{
    public class LexemeNotFoundException : Exception
    {
        public LexemeNotFoundException()
        {
        }

        public LexemeNotFoundException(string message) : base(message)
        {
        }

        public LexemeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
