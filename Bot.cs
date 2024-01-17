using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Bot
    {
        public static Move ChooseComputerMove(Board Board)
        {
            Random rnd = new Random();
            int color = 16;
            List<Move> possibleMoves = GetAllPosibleMovesForColor(color);
            return possibleMoves[rnd.Next(0, possibleMoves.Count)];
        }

        public static List<Move> GetAllPosibleMovesForColor(int color)
        {
            List<Move> possibleMoves = new List<Move>();
            for (int i = 0; i < Board.Square.Length; i++)
            {
                if (Piece.IsColor(Board.Square[i], color))
                {
                    for (int targetSquare = 0; targetSquare < Board.Square.Length; targetSquare++)
                    {
                        if (Move.IsMoveValid(Board.Square[i], i, targetSquare))
                        {
                            possibleMoves.Add(new Move(i, targetSquare, Board.Square[i]));
                        }
                    }
                }
            }
            return possibleMoves;
        }
    }
}
