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
            int Direction = 0;
            bool validWriting = false;
            for (int i = 0; i < validDirections.Length; i++)
            {
                if (startSquare + validDirections[i] == targetSquare) 
                {
                    validWriting = true;
                    Direction = validDirections[i];
                    break;
                }
            }
            if (validWriting)
            {
                if (Direction == 8)
                    return targetPiece == Piece.None;
                else if (Direction == 16)
                    return targetPiece == Piece.None && (startSquare / 8) + 1 == 2;
                else
                    return (targetPiece % 8 < 16) || unPassont();
            }
            return false;
        }
        public static bool unPassont(){
            return false; 
        }
    }
}
