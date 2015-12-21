using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
namespace VierGewinnt.Rest.logic
{
    public class DtoSessionStatus
    {

        public string PlayerA { get; private set; }
        public string PlayerB { get; private set; }
        public int BoardWidth { get; private set; }
        public int BoardHeight { get; private set; }

        public string Status { get; private set; }

        public DtoSessionStatus(string playerA, string playerB, int boardWidth , int boardHeight, string status)
        {
            this.PlayerA = playerA;
            this.PlayerB = playerB;
            this.BoardWidth = boardWidth;
            this.BoardHeight = boardHeight;
            this.Status = status;
        }

    }
}
