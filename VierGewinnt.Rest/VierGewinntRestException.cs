using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Rest
{
    public class VierGewinntRestException : Exception
    {
        public VierGewinntRestException() : base(){

        }

        public VierGewinntRestException(String message) : base(message)
        {

        }
    }
}
