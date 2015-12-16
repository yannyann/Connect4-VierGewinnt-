using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Model
{
    public class Grid
    {
        private readonly string[] Cells;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string[] ImmutableCells
        {
            get
            {
                string[] target = new string[Cells.Length];
                Array.Copy(Cells, target, Cells.Length);
                return target;
            }
        }

        /// <summary>
        /// Can Throw a GridException if the width or the height are negative
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Grid(int width, int height)
        {
            AssertGridDimension(width, height);
            this.Width = width;
            this.Height = height;
            Cells = new string[this.Width * this.Height];
        }

     /*   public string GetTokenAtPosition(int x, int y)
        {

        }*/

        /// <summary>
        /// put the token in the column at the position X
        /// Throw a GridException if the column position is wrong or the column is full.
        /// </summary>
        /// <param name="x">column position</param>
        /// <param name="token">token</param>
        public void dropToken(int x, string token)
        {
            AssertColumnInTheGrid(x);
            AssertHasStillPlace();
            AssertColumnNotFull(x);
            var yNextPosition = YPositionOfTheNextToken(x);
            Cells[yNextPosition * this.Width + x] = token;
        }
        public string fourInLine()
        {
            string[] extendsGrid = new string[(Width + 6) * (Height + 6)];
            //create an extends Grid to process calculation
            for (int i = 3; i < Width + 3; i++)
            {
                for (int j = 3; j < Height + 3; j++)
                {
                    extendsGrid[i + j * (Width + 6)] = Cells[(i - 3) + (j - 3) * Width];
                }
            }
            //search for each position if we can found a motif
            for (int i = 3; i < Width + 3; i++)
            {
                for (int j = 3; j < Height + 3; j++)
                {
                    string motif = findMotif(extendsGrid, i, j, Width + 6);
                    if (motif != null)
                    {
                        return motif;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// return the Y position of the highest token for the column X. 
        /// Return 0 if no Token was found. 
        /// Throw a GridException if the column position is wrong or the column is full.
        /// </summary>
        /// <param name="x">column position</param>
        /// <returns>the Y position of the highest token for the column X or -1 if no Token was found</returns>
        private int YPositionOfTheNextToken(int x)
        {
            var nextPosition = YPositionOfTheFirstToken(x) + 1;
            if (nextPosition >= this.Height)
            {
                throw new GridException(string.Format("the column {0} cannot be choosed because it is full.", x));
            }
            return nextPosition;
        }

        /// <summary>
        /// return the Y position of the highest token for the column X. 
        /// Return -1 if no Token was found. 
        /// Throw a GridException if the column position is wrong.
        /// </summary>
        /// <param name="x">column position</param>
        /// <returns>the Y position of the highest token for the column X or -1 if no Token was found</returns>
        private int YPositionOfTheFirstToken(int x)
        {
            AssertColumnInTheGrid(x);
            var indexes = Cells.Where((c, i) => i % Width == x && c != null).Select((c, i) => i);
            if (indexes.Any())
            {
                return indexes.Max();
            }
            return -1;
        }

        public bool hasStillPlace()
        {
            bool hasStillPlace = false;
            for (int i = 0; i < Width; i++)
            {
                hasStillPlace = hasStillPlace || Cells[i + (Height - 1) * Width] == null;
            }
            return hasStillPlace;
        }

        private string findMotif(string[] cells, int posx, int posy,int width)
        {
            string verticalMotif = findMotif(cells, posx - 3 + posy * width, posx - 2 + posy * width,
                posx - 1 + posy * width, posx + posy * width);
            if (verticalMotif != null)
            {
                return verticalMotif;
            }

            string horizontalMotif = findMotif(cells, posx + (posy - 3) * width, posx + (posy - 2) * width,
                posx + (posy - 1) * width, posx + posy * width);
            if (horizontalMotif != null)
            {
                return horizontalMotif;
            }

            string diagonal_N_E_Motif = findMotif(cells, posx - 3 + (posy - 3) * width, posx - 2 + (posy - 2) * width,
                posx - 1 + (posy - 1) * width, posx + posy * width);
            if (diagonal_N_E_Motif != null)
            {
                return diagonal_N_E_Motif;
            }

            string diagonal_N_W_Motif = findMotif(cells, posx - 3 + (posy + 3) * width, posx - 2 + (posy + 2) * width,
                posx - 1 + (posy + 1) * width, posx + posy * width);
            if (diagonal_N_W_Motif != null)
            {
                return diagonal_N_W_Motif;
            }

            return null;
        }

        private string findMotif(string[] cells, int x1, int x2, int x3, int x4)
        {
            if (cells[x1] == cells[x2] && cells[x2] == cells[x3] && cells[x3] == cells[x4] && cells[x1] != null)
            {
                return cells[x1];
            }
            return null;
        }
        private void AssertGridDimension(int width, int height)
        {
            if (width <= 0)
            {
                throw new GridException("The grid's width must be >= 0");
            }
            if (height <= 0)
            {
                throw new GridException("The grid's height must be >= 0");
            }
        }
        private void AssertHasStillPlace()
        {
            if (!hasStillPlace())
            {
                throw new GridException("The grid is full");
            }
        }
        private void AssertColumnInTheGrid(int x)
        {
            if (x < 0 || x >= this.Width)
            {
                throw new ColumnOutOfGridException(string.Format("The column {0} cannot be choosed because it doesn't belong to the grid size.", x));
            }
        }

        private void AssertColumnNotFull(int x)
        {
            if (Cells[this.Width * (this.Height - 1) + x] != null)
            {
                throw new FullColumnGridException(string.Format("the column {0} cannot be choosed because it is full.", x));
            }
        }

    }
}
