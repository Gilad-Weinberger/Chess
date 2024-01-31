using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class ValidateMove
    {
        public static bool SlidingPieces(int[] validDirections, int startSquare, int targetSquare, bool CheckChecks)
        {
            for (int i = 0; i < validDirections.Length; i++)
            {
                if ((targetSquare - startSquare) % validDirections[i] == 0)
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
                            if (CheckChecks)
                            {
                                Move moveToVerify = new Move(startSquare, targetSquare, Board.Square[startSquare], false);
                                return CheckForChecks(moveToVerify);
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else if (position != startSquare && position != targetSquare && Board.Square[position] > 0)
                        {
                            break;
                        }
                        char currentFile = Board.IndexToChessPosition(position)[0];
                        if (position != startSquare && (currentFile == 'a' || currentFile == 'h'))
                        {
                            return false;
                        }
                        position += validDirections[i];
                        if (position + 1 % 8 == 0)
                        {
                            break;
                        }
                    }
                }
            }
            return false;
        }
        public static bool KingOrKnight(int[] validDirections, int startSquare, int targetSquare, bool CheckChecks)
        {
            for (int i = 0; i < validDirections.Length; i++)
            {
                if (startSquare + validDirections[i] < 64)
                {
                    if (startSquare + validDirections[i] == targetSquare)
                    {
                        if (Math.Abs(validDirections[i]) < 15)
                        {
                            int startRank = Board.IndexToChessPosition(startSquare)[1];
                            int targetRank = Board.IndexToChessPosition(targetSquare)[1];
                            if (Math.Abs(targetRank - startRank) != 1)
                                return false;
                        }
                        if (CheckChecks)
                        {
                            Move moveToVerify = new Move(startSquare, targetSquare, Board.Square[startSquare], false);
                            if (IsPieceOnTheEdge(startSquare, targetSquare))
                                return CheckForChecks(moveToVerify);
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool IsPieceOnTheEdge(int startSquare, int targetSquare)
        {
            if (Board.IndexToChessPosition(startSquare)[0] == 'a' && Board.IndexToChessPosition(targetSquare)[0] == 'h')
                return false;
            if (Board.IndexToChessPosition(startSquare)[0] == 'h' && Board.IndexToChessPosition(targetSquare)[0] == 'a')
                return false;
            return true;
        }

        public static bool Pawn(int[] validDirections, int startSquare, int targetSquare, bool CheckChecks)
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
                Move.Error = "There is a piece in your target square";
                if (Math.Abs(Direction) == 8)
                {
                    if (targetPiece == Piece.None)
                    {
                        CheckForBecomingQueen(startSquare, targetSquare);
                        if (CheckChecks) { 
                            Move moveToVerify = new Move(startSquare, targetSquare, Board.Square[startSquare], false);
                            return CheckForChecks(moveToVerify);
                        }
                        else { 
                            return true;
                        }
                    }
                }
                else if (Math.Abs(Direction) == 16)
                {
                    if (thisPiece > 16)
                    {
                        if (targetPiece == Piece.None && Board.GetRankOfPosition(startSquare) == 7)
                        {
                            if (CheckChecks)
                            {
                                Move moveToVerify = new Move(startSquare, targetSquare, Board.Square[startSquare], false);
                                return CheckForChecks(moveToVerify);
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (targetPiece == Piece.None && Board.GetRankOfPosition(startSquare) == 2)
                        {
                            if (CheckChecks)
                            {
                                Move moveToVerify = new Move(startSquare, targetSquare, Board.Square[startSquare], false);
                                return CheckForChecks(moveToVerify);
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    Move.Error = "You can't eat an empty square";
                    if (Board.Square[targetSquare] > 16 || EnPassant(startSquare))
                    {
                        Move.Error = "You can't eat an empty";
                        CheckForBecomingQueen(startSquare, targetSquare);
                        if (CheckChecks)
                        {
                            Move moveToVerify = new Move(startSquare, targetSquare, Board.Square[startSquare], false);
                            return CheckForChecks(moveToVerify);
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool EnPassant(int startSquare)
        {
            if (Board.GameMoves.Count < 1)
                return false;
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
            if (Board.GetRankOfPosition(targetSquare) == 8 && Board.Square[startSquare] > 16)
            {
                Console.WriteLine("black queen");
                Board.Square[startSquare] = Piece.Queen | Piece.Black;
            }
            else if (Board.GetRankOfPosition(targetSquare) == 1 && Board.Square[startSquare] < 16)
            {
                Console.WriteLine("white queen");
                Board.Square[startSquare] = Piece.Queen | Piece.White;
            }
        }

        public static bool CheckForChecks(Move moveToVerify)
        {
            Board.MakeMove(moveToVerify);
            int opponentKingPosition = (Board.ColorToMove % 16 != Piece.White) ? Board.BlackKingPosition : Board.WhiteKingPosition;

            bool isChecked = Bot.GetAllPosibleMovesForColor(Board.ColorToMove + 8, false, false)
                .Any(move => move.targetSquare == opponentKingPosition);

            Board.UnmakeMove(moveToVerify);
            Move.Error = "This Move allow your oppnent to eat your king";
            return !isChecked;
        }
    }
}