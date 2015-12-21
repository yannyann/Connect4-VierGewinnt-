using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VierGewinnt.Rest.logic;

namespace VierGewinnt.Rest
{
    public interface IVierGewinnt
    {
        string CreateSession(DtoSessionStart sessionStart);
        SessionStatus Play(Move move);
        SessionStatus Status(string status);
        IEnumerable<SessionStatus> Moves(string sessionId);
    }
}
