using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Model
{
    public class Batch
    {
        private Game game;
        private bool running;
        public void startGame()
        {
            running = true;
            game = new Game();
            createGrid();
            addPlayer();
            gameLoop();

        }

        private void play()
        {
            Console.WriteLine("\n{0} bitte Spielspalte angeben : ", game.getCurrentPlayerName());
            int column = -1;
            try
            {
                column = int.Parse(waitForText()) - 1;
                game.play(column);
                showGrid();
            }
            catch (ColumnOutOfGridException e)
            {
                Console.WriteLine(string.Format("The column {0} cannot be choosed because it doesn't belong to the grid size.", column + 1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void testWin()
        {
            string name = game.getWinnerName();
            if (name != null)
            {
                running = false;
                Console.WriteLine(string.Format("{0} hat gewonnen.", name));
                Console.WriteLine("Enter a key");
                waitForText();
            }
            bool hasStillPlace = game.hasStillPlace();
            if (!hasStillPlace)
            {
                running = false;
                Console.WriteLine("The game is finished. Nobody has won.");
                Console.WriteLine("Enter a key");
                waitForText();
            }
        }

        private void gameLoop()
        {
            try
            {
                while (running)
                {
                    play();
                    testWin();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                gameLoop();
            }
        }

        private void createGrid()
        {
            Console.WriteLine("Enter the grid dimension   => --   width x height");
            string dimension = waitForText();
            if (string.IsNullOrEmpty(dimension))
            {
                Console.WriteLine(string.Format("The grid was set to {0} x {1}", game.getWidth(), game.getHeight()));
                return;
            }
            String[] dim = dimension.Split('x');
            int width;
            int height;
            try
            {
                width = int.Parse(dim[0].Trim());
                height = int.Parse(dim[1].Trim());
                if (width > 500 || height > 500)//not in the spec
                {
                    Console.WriteLine("The maximal dimension is 500 x 500.");
                }
                else
                {
                    game.newGrid(width, height);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("The given width x height are not correct. We will use the default dimension {0} x {1}", Game.MIN_WIDTH, Game.MIN_HEIGHT));
            }

            Console.WriteLine(string.Format("The grid was set to {0} x {1}", game.getWidth(), game.getHeight()));
        }

        private void addPlayer()
        {
            addPlayer("Enter first player's name", 0);
            addPlayer("Enter Second player's name", 1);
        }


        private void addPlayer(string text, int playerNumber)
        {
            if (game.PlayerNumber() == playerNumber)
            {
                Console.WriteLine(text);
                string playerName = waitForText();
                if (!string.IsNullOrEmpty(playerName))
                {
                    game.addPlayer(playerName);
                }
                else
                {
                    game.addPlayer(null);
                }
            }
        }

        private void showGrid()
        {
            Console.WriteLine();
            for (int j = game.getHeight() - 1; j >= 0; j--)
            {
                Console.Write("|");
                for (int i = 0; i < game.getWidth(); i++)
                {
                    Console.Write(string.Format(" {0,1} ", game.getImmutableCells()[i + j * game.getWidth()]));
                }
                Console.Write("|");
                Console.WriteLine();
            }

            Console.Write("|");
            for (int i = 0; i < game.getWidth(); i++)
            {
                Console.Write(string.Format(" {0,1} ", "_"));
            }
            Console.WriteLine("|");

            Console.Write(" ");
            for (int i = 0; i < game.getWidth(); i++)
            {
                Console.Write(string.Format(" {0,1} ", i + 1));
            }
            Console.WriteLine(" ");
        }
        private string waitForText()
        {
            return Console.ReadLine();
        }


        public static void Main(string[] args)
        {
            Batch batch = new Batch();
            batch.startGame();
            batch.showGrid();

        }
    }
}
