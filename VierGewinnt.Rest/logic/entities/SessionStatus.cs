using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
namespace VierGewinnt.Rest.logic
{
    public class SessionStatus
    {
        public enum State
        {
            AwaitingMovePlayerA,
            AwaitingMovePlayerB,
            WinnerIsPlayerA,
            WinnerIsPlayerB
        }

        public string PlayerA { get; private set; }
        public string PlayerB { get; private set; }
        public int BoardWidth { get; private set; }
        public int BoardHeight { get; private set; }

        public State Status { get; private set; }

        public SessionStatus(string playerA, string playerB, int boardWidth , int boardHeight, State status)
        {
            this.PlayerA = playerA;
            this.PlayerB = playerB;
            this.BoardWidth = boardWidth;
            this.BoardHeight = boardHeight;
            this.Status = status;
        }

    }
}
