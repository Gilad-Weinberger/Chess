using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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

            while ((!Board.checkmate) & (!Board.draw))
            {
                string Color = (Board.ColorToMove == Piece.White) ? "White" : "Black";
                if (Board.ColorToMove % 16 != 0)
                {
                    while (true)
                    {
                        Console.WriteLine($"Enter White's Move:");
                        string MoveText = Console.ReadLine();
                        int startSquare = int.Parse(MoveText[1].ToString()) * 8 - (8 - (Convert.ToInt32(MoveText[0]) - 97));
                        int targetSquare = int.Parse(MoveText[3].ToString()) * 8 - (8 - (Convert.ToInt32(MoveText[2]) - 97));
                        int piece = Board.Square[startSquare];

                        Move move = new Move(startSquare, targetSquare, piece, true);

                        if (Move.IsMoveValid(move.piece, move.startSquare, move.targetSquare, true))
                        {
                            Move.Print(move);
                            Board.MakeMove(move);
                            Board.GameMoves.Add(move);
                            break;
                        }

                        Console.WriteLine($"{MoveText} is and invalid move");
                        Console.WriteLine(Move.Error);
                    }
                }
                else
                {
                    Move blackMove = new Move(Bot.ChooseComputerMove(), false);
                    Move.Print(blackMove);
                    Console.WriteLine(); 
                    Board.MakeMove(blackMove);
                    Board.GameMoves.Add(blackMove);
                    DrawBoard(Board.Square);
                }
                Board.ColorToMove += 8;
            }

            if (Board.checkmate)
            {
                string winnerColor = (Board.winner == Piece.White) ? "White" : "Black";
                Console.WriteLine($"CheckMate! {winnerColor} wins!");
            }
            else if (Board.draw)
            {
                Console.WriteLine("It's a draw!");
            }
        }

        public static void DrawBoard(int[] Square)
        {
            Console.WriteLine();
            printFilesLetters();
            for (int rank = 7; rank >= 0; rank--)
            {
                Console.WriteLine($"        {string.Concat(Enumerable.Repeat("-", 33))}");
                Console.Write($"      {rank + 1} ");
                for (int file = 0; file < 8; file++)
                {
                    Console.Write($"|");
                    int piece = Square[rank * 8 + file];
                    if ((file + 7 - rank) % 2 == 0)
                        Console.BackgroundColor = ConsoleColor.Magenta;
                    else
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    if (piece < 16)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.Write($" {Piece.GetPieceSymbol(piece)} ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write($"| {rank + 1}");
                Console.WriteLine();
            }
            Console.WriteLine($"        {string.Concat(Enumerable.Repeat("-", 33))}");
            printFilesLetters();
            Console.WriteLine();
        }

        public static void printFilesLetters()
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
