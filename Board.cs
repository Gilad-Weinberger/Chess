using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Board
    {
        public static int[] Square = new int[64];
        public static List<Move> GameMoves = new List<Move>();

        public static int ColorToMove = 8, friendlyColor = 8, opponentColor = 16;
        public static bool whiteInChcek = false, blackInCheck = false;
        public static int WhiteKingPosition = ChessPositionToIndex("e1"), BlackKingPosition = ChessPositionToIndex("e8");
        public static int PawnsRank = 2;
        public static bool checkmate = false, draw = false;
        public int winner = 1;

        public static void MakeMove(Move move)
        {
            int PieceToMove = Board.Square[move.startSquare];
            Board.Square[move.startSquare] = 0;
            Board.Square[move.targetSquare] = PieceToMove;
        }

        public static void UnmakeMove(Move move, int pieceInTargetSquare)
        {
            int PieceToUnmove = Board.Square[move.targetSquare];
            Board.Square[move.targetSquare] = pieceInTargetSquare;
            Board.Square[move.startSquare] = PieceToUnmove;
        }

        public static string IndexToChessPosition(int index)
        {
            char file = (char)('a' + index % 8);
            int rank = (index / 8) + 1;

            return $"{file}{rank}";
        }

        public static int ChessPositionToIndex(string position)
        {
            char file = position[0];
            int rank = int.Parse(position[1].ToString());

            return rank * 8 + (file - 'a') - 8;
        }

        public static int GetFileNumberOfPosition(int position)
        {
            return position % 8;
        }

        public static int GetRankOfPosition(int position)
        {
            return (position / 8) + 1;
        }

        public static void LoadPositionFromFen(string fen)
        {
            var pieceTypeFromSymbol = new Dictionary<char, int>()
            {
                ['k'] = Piece.King,
                ['p'] = Piece.Pawn,
                ['n'] = Piece.Knight,
                ['b'] = Piece.Bishop,
                ['r'] = Piece.Rook,
                ['q'] = Piece.Queen
            };

            string fenBoard = fen.Split(' ')[0];
            int file = 0, rank = 7;

            foreach (char symbol in fenBoard)
            {
                if (symbol == '/')
                {
                    file = 0;
                    rank--;
                }
                else
                {
                    if (char.IsDigit(symbol))
                    {
                        file += (int)char.GetNumericValue(symbol);
                    }
                    else
                    {
                        int pieceColor = (char.IsUpper(symbol) ? Piece.White : Piece.Black);
                        int pieceType = pieceTypeFromSymbol[char.ToLower(symbol)];
                        Square[rank * 8 + file] = pieceType | pieceColor;
                        file++;
                    }
                }
            }
        }
    }
}
