using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VierGewinnt.Model;
using VierGewinnt.Rest.DTO;
using VierGewinnt.Rest.logic;

namespace VierGewinnt.Rest
{
    public class RestLogic : IVierGewinnt
    {

        Dictionary<string, Context> Contexes { get; set; }

        public int MAX_WIDTH { get; set; }
        private int MAX_HEIGHT { get; set; }

        public RestLogic()
        {
            Contexes = new Dictionary<string, Context>();
            MAX_WIDTH = 500;
            MAX_HEIGHT = 500;
        }

        public string CreateSession(DtoSessionStart sessionStart)
        {
           // assertDimension(sessionStart);

            Game game = new Game();
            game.newGrid(sessionStart.BoardWidth ?? 7, sessionStart.BoardHeight ?? 6);
            game.addPlayer(sessionStart.PlayerA);
            game.addPlayer(sessionStart.PlayerB);
            if (game == null)
            {
                return null;
            }
            string sessionid = RandomString(16);
            Contexes.Add(sessionid, new Context(game,
                new SessionStatus(
                    game.getPlayerNameA(), game.getPlayerNameB(), game.getWidth(),
                    game.getHeight(), SessionStatus.State.AwaitingMovePlayerA)));
            return sessionid;
        }

      /*  private void assertDimension(DtoSessionStart sessionStart)
        {
            if (sessionStart.BoardWidth > MAX_WIDTH)
            {
                throw new LogicException(string.Format("The width must be <={0}",MAX_WIDTH));
            }else if(sessionStart.BoardHeight > MAX_HEIGHT)
            {
                throw new LogicException(string.Format("The height must be <={0}", MAX_HEIGHT));
            }

        }*/

        public IEnumerable<SessionStatus> Moves(string sessionId)
        {
            return Contexes[sessionId].Moves;
        }

        public SessionStatus.State Play(string sessionId, Move move)
        {
            var context = Contexes[sessionId];
            var game = context.Game;
            
            game.play(move.PlayerName, move.Column);

            if (game.IsFinished())
            {
                if(game.getWinnerName() == game.getPlayerNameA())
                {
                    return SessionStatus.State.WinnerIsPlayerA;
                }
                else if (game.getWinnerName() == game.getPlayerNameB())
                {
                    return SessionStatus.State.WinnerIsPlayerB;
                }
            }
            else if (game.getCurrentPlayerName() == game.getPlayerNameA())
            {
                return SessionStatus.State.AwaitingMovePlayerA;
            }
            else if (game.getCurrentPlayerName() == game.getPlayerNameB())
            {
                return SessionStatus.State.AwaitingMovePlayerB;
            }

            throw new GameException("Unknown state");
        }

        public SessionStatus Status(string sessionid)
        {
            return Contexes[sessionid].Status;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
