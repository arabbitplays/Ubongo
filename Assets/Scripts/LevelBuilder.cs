using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private int boardID;
    [SerializeField] private List<PieceBuilder> pieceBuilders;

    public Board buildBoard()
    {
        BoardFactory boardFactory = new BoardFactory();
        switch(boardID)
        {
            case 1: return boardFactory.getTest1Board();
            case 2: return boardFactory.getTest2Board();
            case 3: return boardFactory.getTest3Board();
            case 4: return boardFactory.getTest4Board();
        }
        return null;
    }

    public List<Piece> buildPieces()
    {
        List<Piece> pieces = new List<Piece>();
        foreach (PieceBuilder builder in pieceBuilders)
        {
            pieces.Add(builder.buildPiece());
        }
        return pieces;
    }
}
