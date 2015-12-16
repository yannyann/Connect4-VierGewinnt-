using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Model
{
    public class WinEventArgs : EventArgs
    {
        public string playerName;

        public WinEventArgs(string playerName)
        {
            this.playerName = playerName;
        }
    }
}
