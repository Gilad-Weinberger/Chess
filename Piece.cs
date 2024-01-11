using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Piece
    {
        public const int None = 0;
        public const int Pawn = 1;
        public const int Rook = 2;
        public const int Knight = 3;
        public const int Bishop = 4;
        public const int Queen = 5;
        public const int King = 6;

        public const int White = 8;
        public const int Black = 16;

        public static char GetPieceSymbol(int pieceValue)
        {
            int pieceType = pieceValue % 8;
            int pieceColor = pieceValue & (Piece.White | Piece.Black);

            switch (pieceType)
            {
                case Piece.None: return ' ';
                case Piece.King: return (pieceColor == Piece.White) ? 'K' : 'k';
                case Piece.Queen: return (pieceColor == Piece.White) ? 'Q' : 'q';
                case Piece.Rook: return (pieceColor == Piece.White) ? 'R' : 'r';
                case Piece.Bishop: return (pieceColor == Piece.White) ? 'B' : 'b';
                case Piece.Knight: return (pieceColor == Piece.White) ? 'N' : 'n';
                case Piece.Pawn: return (pieceColor == Piece.White) ? 'P' : 'p';
                default: return ' ';
            }
        }

        public static bool IsColor(int piece, int color)
        {
            int pieceColor = piece & (Piece.White | Piece.Black);

            if (color == White)
                return pieceColor == White;
            return pieceColor == Black;
        }
    }
}
