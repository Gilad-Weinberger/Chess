using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Bot
    {
        public static Move ChooseComputerMove()
        {
            Random rnd = new Random();
            int color = 16;
            List<Move> possibleMoves = GetAllPosibleMovesForColor(color, true, true);
            Console.WriteLine($"\nThere are {possibleMoves.Count} possible moves for Black\n");
            if (possibleMoves.Count == 0)
            {
                Board.checkmate = true;
                return new Move(-1, -1, -1, false);
            }
            return possibleMoves[rnd.Next(0, possibleMoves.Count)];
        }


        public static List<Move> GetAllPosibleMovesForColor(int color, bool CheckChecks, bool PrintMoves)
        {
            int moveNumber = 1;
            if (PrintMoves)
                Console.WriteLine("Black Possible Moves:\n");
            List<Move> possibleMoves = new List<Move>();
            for (int i = 0; i < Board.Square.Length; i++)
            {
                if (Piece.IsColor(Board.Square[i], color))
                {
                    for (int targetSquare = 0; targetSquare < Board.Square.Length; targetSquare++)
                    {
                        if (Move.IsMoveValid(Board.Square[i], i, targetSquare, CheckChecks))
                        {
                            if (PrintMoves)
                            {
                                Console.Write(moveNumber + ": ");
                                Move.Print(Board.Square[i], i, targetSquare);
                                if (Board.Square[i] % 8 == 4 || Board.Square[i] % 8 == 3)
                                {
                                    Console.WriteLine(Math.Abs(targetSquare - i));
                                }
                            }
                            moveNumber++;
                            possibleMoves.Add(new Move(i, targetSquare, Board.Square[i], false));
                        }
                    }
                }
            }
            return possibleMoves;
        }
    }
}
