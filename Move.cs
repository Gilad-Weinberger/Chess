using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Move
    {
        public static string Error = "";
        public int startSquare, targetSquare;
        public int piece;

        public Move(int startSquare, int targetSquare, int piece, bool CheckForLosingKing)
        {
            if (CheckForLosingKing)
            {
                if (IsMoveValid(piece, startSquare, targetSquare, true))
                {
                    this.startSquare = startSquare;
                    this.targetSquare = targetSquare;
                    this.piece = piece;
                }
            }
            else
            {
                this.startSquare = startSquare;
                this.targetSquare = targetSquare;
                this.piece = piece;
            }
        }
        public Move(Move move, bool CheckForLosingKing)
        {
            if (CheckForLosingKing)
            {
                if (IsMoveValid(move.piece, move.startSquare, move.targetSquare, true))
                {
                    this.startSquare = move.startSquare;
                    this.targetSquare = move.targetSquare;
                    this.piece = move.piece;
                }
            }
            else
            {
                this.startSquare = move.startSquare;
                this.targetSquare = move.targetSquare;
                this.piece = move.piece;
            }
        }

        public static void Print(Move move)
        {
            Console.WriteLine(Piece.GetPieceSymbol(move.piece) + ": " + Board.IndexToChessPosition(move.startSquare) + " -> " + Board.IndexToChessPosition(move.targetSquare));
        }

        public static void Print(int piece, int startSquare, int targetSquare)
        {
            Console.WriteLine(Piece.GetPieceSymbol(piece) + ": " + Board.IndexToChessPosition(startSquare) + " -> " + Board.IndexToChessPosition(targetSquare));
        }

        public static bool IsMoveValid(int piece, int startSquare, int targetSquare, bool CheckForLosingKing)
        {
            if (piece == Board.Square[targetSquare] || startSquare == targetSquare)
            {
                Move.Error = "You must move the piece";
                return false;
            }
            if (Piece.IsColor(Board.Square[targetSquare], Piece.GetColor(piece)))
            {
                Move.Error = $"You can't land on your own pieces {Board.Square[targetSquare]} - {Board.ColorToMove}";
                return false;
            }

            int pieceType = piece % 8;

            switch (pieceType)
            {
                case Piece.King: return ValidateMove.KingOrKnight(new int[] { 8, -8, 1, -1, 7, -7, 9, -9 }, startSquare, targetSquare, CheckForLosingKing);
                case Piece.Queen: return ValidateMove.SlidingPieces(new int[] { 8, -8, 1, -1, 7, -7, 9, -9 }, startSquare, targetSquare, CheckForLosingKing);
                case Piece.Rook: return ValidateMove.SlidingPieces(new int[] { 8, -8, 1, -1 }, startSquare, targetSquare, CheckForLosingKing);
                case Piece.Bishop: return ValidateMove.SlidingPieces(new int[] { 7, -7, 9, -9 }, startSquare, targetSquare, CheckForLosingKing);
                case Piece.Knight: return ValidateMove.KingOrKnight(new int[] { 15, 17, 6, 10, -6, -10, -15, -17}, startSquare, targetSquare, CheckForLosingKing);
                case Piece.Pawn: return ValidateMove.Pawn(new int[] { 8, 16, 7, 9, -8, -16, -9, -7 }, startSquare, targetSquare, CheckForLosingKing);
                default: return false;
            }
        }
    }
}
