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
            const string startFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
            Board Board = new Board();
            Board.LoadPositionFromFen(startFen);
            DrawBoard(Board.Square);
        }

        public static void DrawBoard(int[] Square)
        {
            Console.WriteLine();
            for (int rank = 0; rank < 8; rank++)
            {
                Console.WriteLine(string.Concat(System.Linq.Enumerable.Repeat("-", 33))); 
                for (int file = 0; file < 8; file++)
                {
                    Console.Write("| " + Piece.GetPieceSymbol(Square[rank * 8 + file]) + " ");
                }
                Console.Write("|");
                Console.WriteLine();
            }
            Console.WriteLine(string.Concat(System.Linq.Enumerable.Repeat("-", 33)));
            Console.WriteLine();
        }
    }
}
