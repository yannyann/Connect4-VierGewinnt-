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

        public void play(int column)
        {
            AssertStillInRunning();
            //AssertExistPlayer(currentPlayer);
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
            if (!string.IsNullOrEmpty(winnerToken))
            {

                return currentPlayer.Name;
            }
            return null;
        }



        public bool IsFinished()
        {
            return getWinnerName() != null||!hasStillPlace();
        }

        static string UppercaseFirst(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return char.ToUpper(s[0]) + s.Substring(1);
            }
            return s;

        }

        private string formatPlayerName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string formattedName = name.Trim();
                formattedName = UppercaseFirst(formattedName);
                return formattedName;
            }
            return name;

        }

        public void addPlayer(string name)
        {
            string formattedName = formatPlayerName(name);
            AssertDontHaveTheTwoPlayers();
            AssertPlayerNameStillExist(name);
            if (string.IsNullOrEmpty(formattedName))
            {
                if (playerA == null)
                {
                    playerA = new Player("Spieler A", "X");
                    currentPlayer = playerA;
                }
                else
                {
                    playerB = new Player("Spieler B", "O");
                }
            }
            else
            {
                if (playerA == null)
                {
                    playerA = new Player(formattedName, "X");
                    currentPlayer = playerA;
                }
                else
                {
                    playerB = new Player(formattedName, "O");
                }
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

        private void changeTurn()
        {
            currentPlayer = currentPlayer == playerA ? playerB : playerA;
        }
        private Player findByToken(string token)
        {
            return getAllPlayers().Single(p => string.Equals(p.Token, token));
        }
        /*private Player findByName(string name)
        {
            string formattedName = formatPlayerName(name);
            AssertNullOrEmptyPlayerName(formattedName);
            if (string.Equals(formattedName, playerA.Name))
            {
                return playerA;
            }
            if (string.Equals(formattedName, playerB.Name))
            {
                return playerB;
            }
            return null;
        }*/

        private void raiseWinEvent(string name)
        {
            if (hasWon!=null)
            {
                hasWon.Invoke(this, new WinEventArgs(name));
            }
        }

        private void AssertStillInRunning()
        {
            if (grid.fourInLine() != null)
            {
                throw new GameException("The Game is finish. four "+ grid.fourInLine()+" have been found");
            }
            if (!grid.hasStillPlace())
            {
                throw new GameException("The Game is finish. No winner.");
            }
        }

        /*private void AssertNullOrEmptyPlayerName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new GameException("The player's name is null");
            }
        }*/

        private void AssertRightGridDimension(int width, int height)
        {
            if (width < MIN_WIDTH)
            {
                throw new GameException(string.Format("The width must be => {0}", MIN_WIDTH));
            }
            if (height < MIN_HEIGHT)
            {
                throw new GameException(string.Format("The width must be => {0}", MIN_HEIGHT));
            }

        }

        private void AssertDontHaveTheTwoPlayers()
        {
            if (playerA != null && playerB != null)
            {
                throw new GameException(string.Format("This game still have its 2 players {0} vs {1}.", playerA.Name, playerB.Name));
            }
        }

        /*private void AssertExistPlayer(Player p)
        {
            if (p == null)
            {
                throw new GameException("The player doesn't exist.");
            }
        }*/

       /* private void AssertIsPlayerTurn(Player p)
        {
            if (p != currentPlayer)
            {
                throw new GameException("It is not the Player " + p + "'s turn");
            }
        }*/

      /*  private void AssertPlayerInTheGame(Player p)
        {
            if (p != playerB && p != playerA)
            {
                throw new GameException("The player " + p + " is not in the game");
            }
        }*/
        private void AssertPlayerNameStillExist(string name)
        {

            if (playerA!=null&&playerA.Name!=null&&string.Equals(name, playerA.Name))
            {
                throw new GameException(string.Format("The player's name {0} still exists", name));
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
