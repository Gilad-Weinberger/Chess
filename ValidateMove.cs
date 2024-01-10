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
                int position = startSquare;
                while (position < 64)
                {
                    if (position == targetSquare)
                        return true;
                    position += validDirections[i];
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
                    if (startSquare + validDirections[i] == startSquare)
                        return true;
                }
            }
            return false;
        }
        public static bool Pawn(int validDirections, int startSquare, int targetSquare)
        {
            int targetPiece = Board.Square[targetSquare];
            int Direction;
            bool validWriting = false;
            for (int i = 0; i < validDirections.Length; i++)
            {
                if (startSquare + validDirections[i] == targetSquare) 
                {
                    validWriting = true;
                    int Direction = validDirections[i];
                    break;
                }


            }
            if (validWriting)
            {
                if (Direction == 8)
                    return targetPiece == Piece.None;
                else if (Direction == 16)
                    return targetPiece == Piece.None && (startSquare % 8) + 1 == 2;
                else
                    return (targetPiece > 8 && targetPiece < 16) || unPassont()
            }
            return false;
        }
        public static bool unPassont(){
            return false 
        }
    }
}
