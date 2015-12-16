using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using VierGewinnt.Model;
namespace VierGewinnt.Tests
{
    public class GridTest
    {
        private readonly Grid grid;
        private int Width;
        private int Height;
        public GridTest()
        {
            this.Width = 6;
            this.Height = 8;
            this.grid = new Grid(Width, Height);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, -1)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        public void Should_Throw_An_Exception_Due_To_Wrong_Dimension(int width, int height)
        {
            Assert.Throws<GridException>(() => { Grid g = new Grid(width, height); });
        }

        [Fact]
        private void Should_Test_Dimension_Grid()
        {
            Assert.Equal(Width * Height, grid.ImmutableCells.Length);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(5)]
        public void Should_Put_Token_In_Right_Column(int xColumn)
        {
            grid.dropToken(xColumn, "X");
            Assert.Equal("X", grid.ImmutableCells[xColumn]);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(6)]
        public void Should_Make_An_Grid_Exception(int xColumn)
        {
            Exception ex = Assert.Throws<ColumnOutOfGridException>(() => grid.dropToken(xColumn, "X"));
            Assert.Equal(string.Format("The column {0} cannot be choosed because it doesn't belong to the grid size.", xColumn), ex.Message);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(5)]
        public void Should_Accept_To_Drop_Until_Height(int xColumn)
        {
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            Assert.Equal("X", grid.ImmutableCells[xColumn]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + Width]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + 2 * Width]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + 3 * Width]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + 4 * Width]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + 5 * Width]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + 6 * Width]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + 7 * Width]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(5)]
        public void Should_Throw_An_Grid_Exception_Because_Column_Is_Full(int xColumn)
        {
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");

            Assert.Equal("X", grid.ImmutableCells[xColumn]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + Width]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + 2 * Width]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + 3 * Width]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + 4 * Width]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + 5 * Width]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + 6 * Width]);
            Assert.Equal("X", grid.ImmutableCells[xColumn + 7 * Width]);

            Exception ex = Assert.Throws<FullColumnGridException>(() => grid.dropToken(xColumn, "X"));
            Assert.Equal(string.Format("the column {0} cannot be choosed because it is full.", xColumn), ex.Message);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(5)]

        public void Should_Valid_Normal_Vertical_Four(int xColumn)
        {
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");

            Assert.Equal("X", grid.fourInLine());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(5)]

        public void Should_Valid_Complex1_Vertical_Four(int xColumn)
        {
            grid.dropToken(xColumn, "O");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");

            Assert.Equal("X", grid.fourInLine());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(5)]

        public void Should_Valid_Limit_Vertical_Four(int xColumn)
        {
            grid.dropToken(xColumn, "O");
            grid.dropToken(xColumn, "O");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");
            grid.dropToken(xColumn, "X");

            Assert.Equal("X", grid.fourInLine());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void Should_Valid_Normal_Horizontal_Four(int yBegin)
        {
            grid.dropToken(yBegin, "X");
            grid.dropToken(yBegin + 1, "X");
            grid.dropToken(yBegin + 2, "X");
            grid.dropToken(yBegin + 3, "X");

            Assert.Equal("X", grid.fourInLine());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]

        public void Should_Valid_Complex1_Horizontal_Four(int yBegin)
        {
            grid.dropToken(yBegin - 1, "O");
            grid.dropToken(yBegin, "X");
            grid.dropToken(yBegin + 1, "X");
            grid.dropToken(yBegin + 2, "X");
            grid.dropToken(yBegin + 3, "X");

            Assert.Equal("X", grid.fourInLine());
        }

        [Theory]
        [InlineData(2)]

        public void Should_Valid_Limit_Horizontal_Four(int yBegin)
        {
            grid.dropToken(yBegin - 2, "O");
            grid.dropToken(yBegin - 1, "O");
            grid.dropToken(yBegin, "X");
            grid.dropToken(yBegin + 1, "X");
            grid.dropToken(yBegin + 2, "X");
            grid.dropToken(yBegin + 3, "X");

            Assert.Equal("X", grid.fourInLine());
        }

        [Fact]
        public void Should_Valid_Normal_Diagonal_N_W_Four()
        {
            grid.dropToken(0, "O");
            grid.dropToken(0, "O");
            grid.dropToken(0, "O");
            grid.dropToken(0, "X");
            grid.dropToken(1, "O");
            grid.dropToken(1, "O");
            grid.dropToken(1, "X");
            grid.dropToken(2, "O");
            grid.dropToken(2, "X");
            grid.dropToken(3, "X");

            Assert.Equal("X", grid.fourInLine());
        }

        [Fact]

        public void Should_Valid_Normal_Diagonal_N_E_Four()
        {

            grid.dropToken(0, "X");
            grid.dropToken(1, "O");
            grid.dropToken(1, "X");
            grid.dropToken(2, "O");
            grid.dropToken(2, "O");
            grid.dropToken(2, "X");
            grid.dropToken(3, "O");
            grid.dropToken(3, "O");
            grid.dropToken(3, "O");
            grid.dropToken(3, "X");

            Assert.Equal("X", grid.fourInLine());
        }


    }
}
