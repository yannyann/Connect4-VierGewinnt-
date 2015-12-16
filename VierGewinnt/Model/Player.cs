using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Model
{
    public class Player
    {
        public string Name { get; set; }
        public string Token { get; set; }

        public Player(string name, string token)
        {
            this.Name = name;
            this.Token = token;
        }
    }
}
