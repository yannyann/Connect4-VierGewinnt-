using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Model
{
    public class Game
    {
        public static readonly int MIN_WIDTH = 7;
        public static readonly int MIN_HEIGHT = 6;

        private Player playerA { get; set; }
        private Player playerB { get; set; }

        private Player currentPlayer { get; set; }

        private Grid grid { get; set; }

        public event EventHandler hasWon;

        public Game()
        {
            newGrid(MIN_WIDTH, MIN_HEIGHT);
        }

        public void newGrid(int width, int height)
        {
            AssertRightGridDimension(width, height);
            grid = new Grid(width, height);
        }

        public void play(string playerName,int column)
        {
            AssertStillInRunning();
            assertPlayerName(playerName);
            AssertYourTurn(playerName);
            grid.dropToken(column, currentPlayer.Token);
            string winnerName = getWinnerName();
            if (!string.IsNullOrEmpty(winnerName))
            {
                raiseWinEvent(winnerName);
            }
            else
            {
                changeTurn();
            }
        }

        public string getWinnerName()
        {
            string winnerToken = grid.fourInLine();
            return !string.IsNullOrEmpty(winnerToken) ? currentPlayer.Name : null;
        }

        public bool IsFinished()
        {
            return getWinnerName() != null || !hasStillPlace();
        }

        static string UppercaseFirst(string s)
        {
            return !string.IsNullOrEmpty(s) ? char.ToUpper(s[0]) + s.Substring(1) : s;
        }

        public void addPlayer(string name)
        {
            string formattedName = formatPlayerName(name);
            AssertDontHaveTheTwoPlayers();
            AssertPlayerNameStillExist(formattedName);

            if (playerA == null)
            {
                playerA = new Player(string.IsNullOrEmpty(formattedName) ? "Spieler A" : formattedName, "X");
                currentPlayer = playerA;
            }
            else
            {
                playerB = new Player(string.IsNullOrEmpty(formattedName) ? "Spieler B" : formattedName, "O");
            }
        }

        public string getCurrentPlayerName()
        {
            return currentPlayer.Name;
        }

        public int PlayerNumber()
        {
            return (playerA != null ? 1 : 0) + (playerB != null ? 1 : 0);
        }

        public string[] getImmutableCells()
        {
            return grid.ImmutableCells;
        }

        public int getWidth()
        {
            return grid.Width;
        }
        public int getHeight()
        {
            return grid.Height;
        }

        public bool hasStillPlace()
        {
            return grid.hasStillPlace();
        }

        public string getPlayerNameA()
        {
            return playerA.Name;
        }

        public string getPlayerNameB()
        {
            return playerB.Name;
        }

        private void changeTurn()
        {
            currentPlayer = currentPlayer == playerA ? playerB : playerA;
        }
        private Player findByToken(string token)
        {
            return getAllPlayers().Single(p => string.Equals(p.Token, token));
        }
        private string formatPlayerName(string name)
        {
            return !string.IsNullOrEmpty(name) ? UppercaseFirst(name.Trim()) : name;
        }

        private void raiseWinEvent(string name)
        {
            if (hasWon != null)
            {
                hasWon.Invoke(this, new WinEventArgs(name));
            }
        }

        private void AssertStillInRunning()
        {
            if (grid.fourInLine() != null)
            {
                throw new GameException("The Game is finish. four " + grid.fourInLine() + " have been found");
            }

            if (!grid.hasStillPlace())
            {
                throw new GameException("The Game is finish. No winner.");
            }
        }

        private void AssertRightGridDimension(int width, int height)
        {
            if (width < MIN_WIDTH)
            {
                throw new GameException(string.Format("The width must be >= {0}", MIN_WIDTH));
            }
            if (height < MIN_HEIGHT)
            {
                throw new GameException(string.Format("The width must be >= {0}", MIN_HEIGHT));
            }

        }

        private void AssertDontHaveTheTwoPlayers()
        {
            if (playerA != null && playerB != null)
            {
                throw new GameException(string.Format("This game already have its 2 players {0} vs {1}.", playerA.Name, playerB.Name));
            }
        }

        private void AssertPlayerNameStillExist(string name)
        {

            if (playerA != null && playerA.Name != null && string.Equals(name, playerA.Name))
            {
                throw new GameException(string.Format("The player's name {0} already exists", name));
            }
        }

        private void AssertYourTurn(string playerName)
        {
            string formattedName = formatPlayerName(playerName);
            if(formattedName==null|| !string.Equals(formattedName,getCurrentPlayerName())){
                throw new GameException(string.Format("{0} It's not your turn!", formattedName));
            }
        }

        private void assertPlayerName(string playerName)
        {
            if (playerName == null)
            {
                throw new GameException("The player name is null.");
            }
            string formattedName = formatPlayerName(playerName);
            if (!string.Equals(formattedName, playerA.Name)&& !string.Equals(formattedName, playerB.Name))
            {
                throw new GameException("The player name doesn't exist!");
            }
        }

        private List<Player> getAllPlayers()
        {
            List<Player> players = new List<Player>();
            players.Add(playerA);
            players.Add(playerB);
            return players;
        }

    }
}
