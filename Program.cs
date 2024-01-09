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
            bool checkmate = false, draw = false;
            int winner = 1;

            const string startFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

            List<Move> moves = Move.GenerateMoves();
            
            Board Board = new Board();
            Board.LoadPositionFromFen(startFen);
            DrawBoard(Board.Square);

            while ((!checkmate) & (!draw))
            {
                int ColorToMove = Board.ColorToMove;
                string Color = (ColorToMove == Piece.White) ? "White" : "Black";
                if (Piece.IsColor(Piece.White, ColorToMove))
                {
                    Console.WriteLine($"Enter {Color}'s Move:");
                    string MoveText = Console.ReadLine();
                    int startSquare = (int.Parse(MoveText[1].ToString()) * 8 - (8 - (Convert.ToInt32(MoveText[0]) - 97)));
                    int targetSquare = (int.Parse(MoveText[3].ToString()) * 8 - (8 - (Convert.ToInt32(MoveText[2]) - 97)));

                    Move move = new Move(startSquare, targetSquare);

                    Board.Move(move);                    

                    Console.Clear();
                    DrawBoard(Board.Square);
                }
                else
                {

                }
            }

            if (checkmate)
            {
                string winnerColor = (winner == Piece.White) ? "White" : "Black";
                Console.WriteLine("CheckMate! {} wins!");
            }
            else if (draw)
            {
                Console.WriteLine("It's a draw!");
            }
        }

        public static void DrawBoard(int[] Square)
        {
            Console.WriteLine();
            printLetters();
            for (int rank = 7; rank >= 0; rank--)
            {
                Console.WriteLine($"        {string.Concat(Enumerable.Repeat("-", 33))}");
                Console.Write($"      {rank + 1}");
                for (int file = 0; file < 8; file++)
                {
                    Console.Write($" | {Piece.GetPieceSymbol(Square[rank * 8 + file])}");
                }
                Console.Write($" | {rank + 1}");
                Console.WriteLine();
            }
            Console.WriteLine($"        {string.Concat(Enumerable.Repeat("-", 33))}");
            printLetters();
            Console.WriteLine();
        }

        public static void printLetters()
        {
            Console.Write("        ");
            for (int i = 1; i <= 8; i++)
            {
                Console.Write($"  {(char)(i + 96)} ");
            }
            Console.WriteLine();
        }
    }
}
