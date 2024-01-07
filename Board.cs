using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Board
    {
        public static int[] Square;

        public Board()
        {
            Square = new int[64];

            Square[0] = Piece.White | Piece.Bishop;
            Square[63] = Piece.Black | Piece.Queen;
            Square[63] = Piece.Black | Piece.Knight;
        }
    }
}
