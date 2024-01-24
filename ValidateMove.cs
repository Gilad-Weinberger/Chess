﻿using System;
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
                    /*Console.WriteLine($"{Board.Square[startSquare]}: {position}");*/
                    position += validDirections[i];
                    if (position + 1 % 8 == 0)
                    {
                        break;
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
                if (Math.Abs(Direction) == 8)
                {
                    if (targetPiece == Piece.None)
                    {
                        CheckForBecomingQueen(thisPiece, targetSquare);
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
                        if (targetPiece == Piece.None && (startSquare / 8) + 1 == 7)
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
                        if (targetPiece == Piece.None && (startSquare / 8) + 1 == 2)
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
                    if ((targetPiece > 16) || EnPassant(startSquare, targetSquare))
                    {
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
        public static bool EnPassant(int startSquare, int targetSquare)
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
            int opponentKingPosition = (Board.ColorToMove % 16 != Piece.White) ? Board.BlackKingPosition : Board.WhiteKingPosition;

            bool isChecked = Bot.GetAllPosibleMovesForColor(Board.ColorToMove + 8, false)
                .Any(move => move.targetSquare == opponentKingPosition);

            Board.UnmakeMove(moveToVerify);
            return !isChecked;
        }
    }
}