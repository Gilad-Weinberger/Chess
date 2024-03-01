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
                                return CheckForLosingKing(moveToVerify);
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
                                return CheckForLosingKing(moveToVerify);
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
                            return CheckForLosingKing(moveToVerify);
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
                                return CheckForLosingKing(moveToVerify) && CheckForPieceInteruptingPawnMovement(startSquare, targetSquare);
                            }
                            else
                            {
                                return CheckForPieceInteruptingPawnMovement(startSquare, targetSquare);
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
                                return CheckForLosingKing(moveToVerify) && CheckForPieceInteruptingPawnMovement(startSquare, targetSquare);
                            }
                            else
                            {
                                return CheckForPieceInteruptingPawnMovement(startSquare, targetSquare);
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
                            return CheckForLosingKing(moveToVerify);
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

        public static bool CheckForPieceInteruptingPawnMovement(int startSquare, int targetSquare)
        {
            if (targetSquare > startSquare)
            {
                for (int i = startSquare + 8; i < targetSquare; i+=8)
                {
                    if (Board.Square[i] > 0)
                    {
                        Move.Error = "There is a piece in the way!";
                        return false;
                    }
                }
                return true;
            }
            else if (targetSquare < startSquare)
            {
                for (int i = startSquare - 8; i > targetSquare; i-=8)
                {
                    if (Board.Square[i] > 0)
                    {
                        Move.Error = "There is a piece in the way!";
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
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

        public static bool CheckForLosingKing(Move moveToVerify)
        {
            int piece = Board.Square[moveToVerify.targetSquare];
            Board.MakeMove(moveToVerify);
            int myKingPosition = (Board.ColorToMove % 16 != Board.friendlyColor) ? Board.BlackKingPosition : Board.WhiteKingPosition;

            bool isChecked = false;
            if (Board.ColorToMove % 16 != Board.friendlyColor)
            {
                List<Move> moves1 = Bot.GetAllPosibleMovesForColor(Board.friendlyColor, false, false, false);
                isChecked = moves1.Any(move => move.targetSquare == myKingPosition);
                foreach (Move move1 in moves1)
                {
                    if (move1.targetSquare == myKingPosition)
                    {
                        isChecked = true;

                        bool isCheckedBefore1 = isChecked;
                        int piece1 = Board.Square[move1.targetSquare];
                        Board.MakeMove(move1);
                        List<Move> moves2 = Bot.GetAllPosibleMovesForColor(Board.opponentColor, false, false, false);

                        foreach (Move move2 in moves2)
                        {
                            bool isCheckedBefore2 = isChecked;
                            int piece2 = Board.Square[move2.targetSquare];
                            Board.MakeMove(move2);
                            List<Move> moves3 = Bot.GetAllPosibleMovesForColor(Board.friendlyColor, false, false, false);
                            foreach (Move move3 in moves3)
                            {
                                if (move3.targetSquare == myKingPosition)
                                    isChecked = true;
                            }
                            Board.UnmakeMove(move2, piece2);
                            if (isChecked && !isCheckedBefore2)
                                isChecked = false;
                        }
                        Board.UnmakeMove(move1, piece1);
                        if (isChecked && !isCheckedBefore1)
                            isChecked = false;
                    }
                }
            }
            else
            {
                List<Move> moves = Bot.GetAllPosibleMovesForColor(Board.opponentColor, false, false, false);
                isChecked = moves.Any(move => move.targetSquare == myKingPosition);
            }

            Board.UnmakeMove(moveToVerify, piece);
            Move.Error = "This Move allow your oppnent to eat your king";
            return !isChecked;
        }
    }
}