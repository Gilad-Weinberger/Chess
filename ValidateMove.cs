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
                    {
                        break;
                    }
                    if (position == targetSquare)
                    {
                        Move moveToVerify = new Move(startSquare, targetSquare, Board.Square[startSquare]);
                        return CheckForChecks(moveToVerify);
                    }
                    else if (position != startSquare && Board.Square[position] > 0)
                    {
                        break; 
                    }
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
                    if (startSquare + validDirections[i] == targetSquare)
                    {
                        Move moveToVerify = new Move(startSquare, targetSquare, Board.Square[startSquare]);
                        return CheckForChecks(moveToVerify);
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
                if (Math.Abs(Direction) == 8)
                {
                    if (targetPiece == Piece.None)
                    {
                        CheckForBecomingQueen(thisPiece, targetSquare);
                        Move moveToVerify = new Move(startSquare, targetSquare, Board.Square[startSquare]);
                        return CheckForChecks(moveToVerify);
                    }
                }
                else if (Math.Abs(Direction) == 16)
                {
                    if (thisPiece > 16)
                    {
                        Move moveToVerify = new Move(startSquare, targetSquare, Board.Square[startSquare]);
                        return targetPiece == Piece.None && (startSquare / 8) + 1 == 7 && CheckForChecks(moveToVerify);
                    }
                    else
                    {
                        Move moveToVerify = new Move(startSquare, targetSquare, Board.Square[startSquare]);
                        return targetPiece == Piece.None && (startSquare / 8) + 1 == 2 && CheckForChecks(moveToVerify);
                    }
                }
                else
                {
                    if ((targetPiece > 16) || EnPassant(startSquare, targetSquare))
                    {
                        Move moveToVerify = new Move(startSquare, targetSquare, Board.Square[startSquare]);
                        CheckForBecomingQueen(startSquare, targetSquare);
                        return CheckForChecks(moveToVerify);
                    }
                }
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

        public static void CheckForBecomingQueen(int startSquare, int targetSquare)
        {
            if ((targetSquare / 8) + 1 == 8)
            {
                if (Board.Square[startSquare] > 16)
                    Board.Square[startSquare] = Piece.Queen | Piece.Black;
                else
                    Board.Square[startSquare] = Piece.Queen | Piece.White;
            }
        }

        public static bool CheckForChecks(Move moveToVerify)
        {
            Board.MakeMove(moveToVerify);
            List<Move> possibleResponseMoves = Bot.GetAllPosibleMovesForColor(Board.ColorToMove + 8);
            for (int i = 0; i < possibleResponseMoves.Count; i++)
            {
                Console.WriteLine(possibleResponseMoves[i].startSquare);
                Console.WriteLine(possibleResponseMoves[i].targetSquare);
                if (((Board.ColorToMove + 8) % 16) != 8) {
                    if (possibleResponseMoves[i].targetSquare == Board.BlackKingPosition)
                    {
                        Board.UnmakeMove(moveToVerify);
                        return false;
                    }
                }
                else
                {
                    if (possibleResponseMoves[i].targetSquare == Board.WhiteKingPosition)
                    {
                        Board.UnmakeMove(moveToVerify);
                        return false;
                    }
                }
            }
            Board.UnmakeMove(moveToVerify);
            return true;
        }
    }
}