using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VierGewinnt.Model;

namespace VierGewinnt.Rest.logic
{
    class Context 
    {
        public Game Game { get; private set; }
        public IEnumerable<SessionStatus> Moves { get; private set; }

        public SessionStatus Status { get; set; }

        public Context(Game game, SessionStatus status)
        {
            this.Moves = new List<SessionStatus>();
            this.Status = status;
        }
    }
}
