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

        public static void LoadPositionFromFen(string fen)
        {
            var pieceTypeFromSymbol = new Dictionary<char, int>()
            {
                ['k'] = Piece.King,
                ['p'] = Piece.Pawn,
                ['n'] = Piece.Knight,
                ['b'] = Piece.Bishop,
                ['r'] = Piece.Rook,
                ['q'] = Piece.Queen
            };

            string fenBoard = fen.Split(' ')[0];
            int file = 0, rank = 7;

            foreach (char symbol in fenBoard)
            {
                if (symbol == '/')
                {
                    file = 0;
                    rank--;
                }
                else
                {
                    if (char.IsDigit(symbol))
                    {
                        file += (int)char.GetNumericValue(symbol);
                    }
                    else
                    {
                        int pieceColor = (char.IsUpper(symbol) ? Piece.White : Piece.Black);
                        int pieceType = pieceTypeFromSymbol[char.ToLower(symbol)];
                        Square[rank * 8 + file] = pieceType | pieceColor;
                        file++;
                    }
                }
            }
        }
    }
}
