using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class ValidateMove
    {
        public static bool SlidingPieces(int[] validDirections, int startSquare, int targetSquare)
        {
            for (int i = 0; i < validDirections.Length; i++)
            {
                int line = startSquare / 8;
                int position = startSquare;
                while (position >= 0 && position < 64)
                {
                    if (Math.Abs(validDirections[i]) == 1 && line != position / 8)
                        break;
                    if (position == targetSquare)
                    {
                        return true;
                    }
                    position += validDirections[i];
                    Console.WriteLine(position + 1);
                }
            }
            return false;
        }
        public static bool KingOrKnight(int[] validDirections, int startSquare, int targetSquare)
        {
            for (int i = 0; i < validDirections.Length; i++)
            {
                if (startSquare + validDirections[i] < 64)
                {
                    if (startSquare + validDirections[i] == targetSquare)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool Pawn(int[] validDirections, int startSquare, int targetSquare)
        {
            int targetPiece = Board.Square[targetSquare];
            int thisPiece = Board.Square[startSquare];

            if (thisPiece > 16)
                validDirections = validDirections.Skip(4).Take(5).ToArray();
            else
                validDirections = validDirections.Take(4).ToArray();

            for (int i = 0; i < validDirections.Length; i++)
            {
                Console.WriteLine(validDirections[i]);
            }

            int Direction = 0;
            bool validWriting = false;
            for (int i = 0; i < validDirections.Length; i++)
            {
                if (startSquare + validDirections[i] == targetSquare) 
                {
                    validWriting = true;
                    Direction = validDirections[i];
                    Console.WriteLine(Direction);
                    break;
                }
            }
            if (validWriting)
            {
                if (Math.Abs(Direction) == 8)
                    return targetPiece == Piece.None;
                else if (Math.Abs(Direction) == 16)
                    if (thisPiece > 16)
                        return targetPiece == Piece.None && (startSquare / 8) + 1 == 7;
                    else
                        return targetPiece == Piece.None && (startSquare / 8) + 1 == 2;
                else
                    return (targetPiece > 16) || EnPassant(startSquare, targetSquare);
            }
            return false;
        }
        public static bool EnPassant(int startSquare, int targetSquare)
        {
            Move lastMove = Board.GameMoves[Board.GameMoves.Count - 1];
            if (lastMove.piece == (Piece.Pawn | Piece.Black)) {
                if ((startSquare / 8) + 1 == 5) {
                    int distance = lastMove.startSquare - startSquare;
                    if ((distance == 15 || distance == 17) && (lastMove.targetSquare / 8) + 1 == 5)
                    {
                        Board.Square[lastMove.targetSquare] = 0;
                        return true;
                    }
                } 
            }
            return false;
        }
    }
}
