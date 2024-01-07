using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board Square = new Board();
            DrawBoard(Square);
        }

        public static void DrawBoard(Board Square)
        {
            for (int rank = 1; rank <= 8; rank++)
            {
                Console.WriteLine(string.Concat(System.Linq.Enumerable.Repeat("-", 33))); 
                for (int file = 1; file <= 8; file++)
                {
                    Console.Write("| " + "b" + " ");
                }
                Console.Write("|   ");
                Console.WriteLine();
            }
            Console.WriteLine(string.Concat(System.Linq.Enumerable.Repeat("-", 33)));

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
                }
            }
        }
    }
}
