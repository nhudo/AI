using System;
using System.Collections.Generic;
using System.Text;

namespace GomokuGame
{
    /// <summary>
    /// Bang luong gia cho the co.
    /// Tinh Kinh nghiem cua tro choi dua vao cac
    /// thiet lap ham trong lop nay !
    /// </summary>
    class EValueBoard
    {   
    // ************ VARIABLE *********************************
        public int Width, Height;
        public int[,] Board;

    // ************ CONSTRUCTOR ******************************
        public EValueBoard(GomokuBoard GBoard)
        {
            Width = GBoard.Width;
            Height = GBoard.Height;
            Board = new int[Height + 2, Width + 2];

            ResetBoard();
        }

    // ************ ADDING FUNCTION **************************
        public void ResetBoard()
        {
            for (int r = 0; r < Height + 2; r++)
                for (int c = 0; c < Width + 2; c++)
                    Board[r, c] = 0;
        }

        public Node GetMaxNode()
        {
            int r, c, MaxValue = 0;
            Node n = new Node();

            for (r = 1; r <= Height; r++)
                for (c = 1; c <= Width; c++)
                    if (Board[r, c] > MaxValue)
                    {
                        n.Row = r; n.Column = c;
                        MaxValue = Board[r, c];
                    }

            return n;
        }
    }
}
