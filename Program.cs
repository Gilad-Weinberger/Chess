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

            while ((!Board.checkmate) & (!Board.draw))
            {
                string Color = (Board.ColorToMove == Piece.White) ? "White" : "Black";
                if (Board.ColorToMove % 16 != 0)
                {
                    while (true)
                    {
                        Console.WriteLine($"Enter {Color}'s Move:");
                        string MoveText = Console.ReadLine();
                        int startSquare = int.Parse(MoveText[1].ToString()) * 8 - (8 - (Convert.ToInt32(MoveText[0]) - 97));
                        int targetSquare = int.Parse(MoveText[3].ToString()) * 8 - (8 - (Convert.ToInt32(MoveText[2]) - 97));
                        int piece = Board.Square[startSquare];

                        Move move = new Move(startSquare, targetSquare, piece);

                        if (move != null)
                        {
                            Board.Move(move);
                            Board.GameMoves.Add(move);
                            break;
                        }

                        Console.WriteLine($"{MoveText} is and invalid move");
                    }
                    Board.ColorToMove += 8;
                }
                else
                {
                    Console.Clear();
                    Move blackMove = new Move(ChooseComputerMove(Board));
                    if (blackMove != null)
                        Console.WriteLine("now");
                    Board.Move(blackMove);
                    Board.GameMoves.Add(blackMove);
                    Board.ColorToMove += 8;
                    Console.WriteLine(Board.ColorToMove);
                    DrawBoard(Board.Square);
                }
            }

            if (Board.checkmate)
            {
                string winnerColor = (Board.winner == Piece.White) ? "White" : "Black";
                Console.WriteLine("CheckMate! {} wins!");
            }
            else if (Board.draw)
            {
                Console.WriteLine("It's a draw!");
            }
        }

        public static Move ChooseComputerMove(Board Board)
        {
            Random rnd = new Random();
            List<Move> possibleMoves = GetAllComputerPosibleMoves();
            int index = rnd.Next(0, possibleMoves.Count);
            Console.WriteLine(possibleMoves.Count);
            Move chosenMove = possibleMoves[index];
            Console.WriteLine($"{chosenMove.piece}, {chosenMove.startSquare}, {chosenMove.targetSquare}");
            return chosenMove;
        }

        public static List<Move> GetAllComputerPosibleMoves()
        {
            List<Move> possibleMoves = new List<Move>();
            for (int i = 0; i < Board.Square.Length; i++)
            {
                if (Board.Square[i] > 16)
                {
                    for (int targetSquare = 0; targetSquare < Board.Square.Length; targetSquare++)
                    {
                        if (Move.IsMoveValid(Board.Square[i], i, targetSquare))
                        {
                            possibleMoves.Add(new Move(Board.Square[i], i, targetSquare));
                            Console.WriteLine($"{Board.Square[i]}, {i}, {targetSquare}");
                        }
                    }
                }
            }
            return possibleMoves;
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
