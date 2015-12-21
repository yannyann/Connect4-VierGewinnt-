using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Rest.logic
{
    class LogicException : Exception
    {
        public LogicException() : base()
        {

        }
        public LogicException(string message) : base(message)
        {

        }
    }
}
