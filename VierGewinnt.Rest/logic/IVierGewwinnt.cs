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
        SessionStatus.State Play(string sessionId, Move move);
        SessionStatus Status(string sessionId);
        IEnumerable<SessionStatus> Moves(string sessionId);
    }
}
