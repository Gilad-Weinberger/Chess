using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Move
    {
        public readonly int startSquare, targetSquare;
        public static int[] DirectionOffSets = { 8, -8, 1, -1, 7, -7, 9, -9 };
        public static int[,] NumSquaresToEdge = PreComputedMoveData();

        public Move(int startSquare, int targetSquare)
        {
            this.startSquare = startSquare;
            this.targetSquare = targetSquare;
        }
        
        public static List<Move> GenerateMoves()
        {
            List<Move> moves = new List<Move>();
            for (int startSquare = 0; startSquare < 64; startSquare++)
            {
                int piece = Board.Square[startSquare];
                if (Piece.IsColor(piece, Board.ColorToMove))
                {
                    /*if (Piece.IsSlidingPiece(piece))
                    {
                        GenerateSlidingMoves(startSquare, piece, moves);
                    }*/
                }
            }

            return moves;
        }

        public static List<Move> GenerateSlidingMoves(int startSquare, int piece, List<Move> moves)
        {
            for (int directionIndex = 0; directionIndex < 8; directionIndex++)
            {
                for (int n = 0; n < NumSquaresToEdge[startSquare, directionIndex]; n++)
                {
                    int targetSquare = startSquare + DirectionOffSets[directionIndex] * (n - 1);
                    int pieceOnTargetSquare = Board.Square[targetSquare];

                    // if the piece try to land on a self-captured piece, break.
                    if (Piece.IsColor(pieceOnTargetSquare, Board.friendlyColor))
                    {
                        break;
                    }

                    moves.Add(new Move(startSquare, targetSquare));

                    if (Piece.IsColor(pieceOnTargetSquare, Board.opponentColor))
                    {
                        break;
                    }
                }
            }

            return moves;
        }

        public static int[,] PreComputedMoveData()
        {
            int[,] numSquaresToEdge = new int[64, 8];
            for (int file = 0; file < 8; file++)
            {
                for (int rank = 0; rank < 8; rank++)
                {
                    int numTop = 7 - rank;
                    int numDown = rank;
                    int numLeft = file;
                    int numRight = 7 - file;

                    int squareIndex = rank * 8 + file;

                    numSquaresToEdge[squareIndex, 0] = numTop;
                    numSquaresToEdge[squareIndex, 1] = numDown;
                    numSquaresToEdge[squareIndex, 2] = numLeft;
                    numSquaresToEdge[squareIndex, 3] = numRight;
                    numSquaresToEdge[squareIndex, 4] = Math.Min(numTop, numLeft);
                    numSquaresToEdge[squareIndex, 5] = Math.Min(numDown, numRight);
                    numSquaresToEdge[squareIndex, 6] = Math.Min(numTop, numRight);
                    numSquaresToEdge[squareIndex, 7] = Math.Min(numDown, numLeft);
                }
            }
            return numSquaresToEdge;
        }
    }
}
