using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using VierGewinnt.Model;
namespace VierGewinnt.Tests
{

    public class GameTest
    {
        private Game game;
        public GameTest()
        {
            game = new Game();
            game.addPlayer(null);
            game.addPlayer(null);
        }

        [Fact]
        public void Should_Write_The_Default_Name_For_First_Player()
        {
            Game g = new Game();
            g.addPlayer(null); 

        }

        [Fact]
        public void Should_Write_The_Default_Name_For_Second_Player()
        {
            Game g = new Game();
            g.addPlayer("A");
             g.addPlayer(null); ;
        }

        [Fact]
        public void Should_Throw_An_Exception_Because_Second_Player_Name_Is_Same_As_First()
        {
            Game g = new Game();
            g.addPlayer("A");
            GameException exception = Assert.Throws<GameException>(() => { g.addPlayer("A"); });
            Assert.Equal(string.Format("The player's name {0} still exists", "A"), exception.Message);
        }

        [Theory]
        [InlineData(0,0)]
        [InlineData(7, 5)]
        [InlineData(6, 6)]
        [InlineData(-1, -1)]
        public void Should_Throw_An_Exception_Due_To_Wrong_Dimension(int width,int height)
        {
            GameException exception = Assert.Throws<GameException>(() => { game.newGrid(width, height); });
        }
        [Fact]
        public void Should_Return_True_Sign_Of_Victory_For_A_Player()
        {
            game.play(0);
            game.play(0);
            game.play(0);
            game.play(0);

            game.play(1);
            game.play(1);
            game.play(1);
            game.play(1);

            game.play(2);
            game.play(2);
            game.play(2);
            game.play(2);

            game.play(3);
            Assert.True(game.IsFinished());
            Assert.Equal("Spieler A", game.getWinnerName());
        }

        [Fact]
        public void Should_Throw_An_Exception_Because_Game_Is_Finished()
        {
            game.play(0);
            game.play(0);
            game.play(0);
            game.play(0);

            game.play(1);
            game.play(1);
            game.play(1);
            game.play(1);

            game.play(2);
            game.play(2);
            game.play(2);
            game.play(2);

            game.play(3);
            Exception ex=Assert.Throws<GameException>(()=>game.play(3));
        }

        [Fact]
        public void Should_Change_Player()
        {
            Assert.Equal("Spieler A", game.getCurrentPlayerName());
            game.play(0);
            Assert.Equal("Spieler B", game.getCurrentPlayerName());
            game.play(0);
            Assert.Equal("Spieler A", game.getCurrentPlayerName());
            game.play(3);
            Assert.Equal("Spieler B", game.getCurrentPlayerName());
        }

        [Fact]
        public void Should_Throw_An_Exception_Because_Grid_Full()
        {


            game.play(0);
            game.play(0);
            game.play(0);
            game.play(0);
            game.play(0);
            game.play(0);

            game.play(1);
            game.play(1);
            game.play(1);
            game.play(1);
            game.play(1);
            game.play(1);

            game.play(2);
            game.play(2);
            game.play(2);
            game.play(2);
            game.play(2);
            game.play(2);

            game.play(4);

            game.play(3);
            game.play(3);
            game.play(3);
            game.play(3);
            game.play(3);
            game.play(3);

            game.play(4);
            game.play(4);
            game.play(4);
            game.play(4);
            game.play(4);

            game.play(5);
            game.play(5);
            game.play(5);
            game.play(5);
            game.play(5);
            game.play(5);

            game.play(6);
            game.play(6);
            game.play(6);
            game.play(6);
            game.play(6);
            game.play(6);

            Exception ex = Assert.Throws<GameException>(() => game.play(6));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void Should_Throw_An_Exception_Because_SomeBody_Still_Win(int column)
        {


            game.play(0);
            game.play(0);
            game.play(0);
            game.play(0);
            game.play(0);
            game.play(0);

            game.play(1);
            game.play(1);
            game.play(1);
            game.play(1);
            game.play(1);
            game.play(1);

            game.play(2);
            game.play(2);
            game.play(2);
            game.play(2);
            game.play(2);
            game.play(2);

            game.play(3);

            Exception ex = Assert.Throws<GameException>(() => game.play(column));
        }
    }
}
