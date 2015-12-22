using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Rest.logic
{
    public class DtoSessionStart
    {


        public string PlayerA { get; set; }
        public string PlayerB { get; set; }
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }


    }
}
