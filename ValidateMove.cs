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
            int targetPiece = Board.Square[targetPiece];
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
            
            }
            return false;
        }
    }
}
