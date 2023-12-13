using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator
{
    public List<Piece> solveBoard(Board board, List<Piece> possiblePieces)
    {
        return solveBoardRecursive(board, possiblePieces, 0, 0);
    }

    private List<Piece> solveBoardRecursive(Board board, List<Piece> possiblePieces, int x, int y)
    {
        if (board.isSolved())
            return new List<Piece>();
        if (board.getSize().y == y)
        {
            return null;
        }

        for (; y < board.getSize().y; y++)
        {
            for (; x < board.getSize().x; x++)
            {
                Vector2 currPos = new Vector2(x, y);

                List<Piece> result;
                foreach (Piece piece in possiblePieces)
                {
                    if (piece.isSetOnBoard())
                        continue;

                    Piece pieceToSet = piece.copy();
                    pieceToSet.setPos(currPos);

                    for (int i = 0; i < 4; i++) //test all four rotations
                    {
                        pieceToSet.rotate();
                        if (board.pieceFits(piece, currPos) && board.setPiece(pieceToSet))
                        {
                            piece.setSetOnBoard(true);
                            result = callRecursiveWithNextSlot(board, possiblePieces, x, y);
                            if (result != null)
                            {
                                result.Add(pieceToSet);
                                return result;
                            }
                            piece.setSetOnBoard(false);
                            board.unsetPiece(pieceToSet);
                        }
                    }
                }
                result = callRecursiveWithNextSlot(board, possiblePieces, x, y);
                if (result != null)
                {
                    return result;
                }
            }
            x = 0;
        }
        if (board.isSolved())
        {
            return new List<Piece>();
        }
        return null;
    }

    private List<Piece> callRecursiveWithNextSlot(Board board, List<Piece> possiblePieces, int x, int y)
    {
        if (x == board.getSize().x - 1)
        {
            return solveBoardRecursive(board, possiblePieces, 0, y + 1);
        }
        else
        {
            return solveBoardRecursive(board, possiblePieces, x + 1, y);
        }
    }
}
