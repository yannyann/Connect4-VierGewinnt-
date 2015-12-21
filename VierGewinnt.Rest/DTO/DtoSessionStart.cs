using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Rest.logic
{
    public class DtoSessionStart
    {


        public string PlayerA { get; private set; }
        public string PlayerB { get; private set; }
        public int BoardWidth { get; private set; }
        public int BoardHeight { get; private set; }

        public DtoSessionStart()
        {

        }

        public DtoSessionStart(string PlayerA, string PlayerB, int BoardWidth , int BoardHeight)
        {
            this.PlayerA = PlayerA;
            this.PlayerB = PlayerB;
            this.BoardWidth = BoardWidth;
            this.BoardHeight = BoardHeight;
        }

    }
}
