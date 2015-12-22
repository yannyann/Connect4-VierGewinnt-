using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
namespace VierGewinnt.Rest.logic
{
    public class DtoSessionStatus
    {

        public string PlayerA { get; set; }
        public string PlayerB { get; set; }
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }

        public string Status { get; set; }

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
