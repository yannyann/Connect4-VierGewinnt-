using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Model
{
    public class GameException : Exception
    {
        public GameException() : base(){
            }
        public GameException(string message) : base(message)
        {
        }

    }
}
