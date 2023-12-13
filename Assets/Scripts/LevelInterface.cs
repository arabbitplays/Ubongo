using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class LevelInterface : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField] private LevelBuilder levelBuilder;


    private void Start()
    {
        Board board = levelBuilder.buildBoard();
        List<Piece> pieces = levelBuilder.buildPieces();

        board.printBoard();
        pieces[0].setPos(new Vector2(0,3));
        pieces[0].rotate();
        pieces[0].rotate();
        pieces[0].rotate();
        //UnityEngine.Debug.Log(board.setPiece(pieces[0]));

        pieces[1].setPos(new Vector2(3, 2));
        UnityEngine.Debug.Log(board.setPiece(pieces[1]));

        board.printBoard();

        pieces[2].setPos(new Vector2(1, 1));
        UnityEngine.Debug.Log(board.setPiece(pieces[2]));
        board.printBoard();
        return;

        List<Piece> result = getResultAndStopTime(board, pieces);
        if (result == null)
        {
            UnityEngine.Debug.Log("NO SOLUTION");
            return;
        }
        showBoard(board);
    }

    private List<Piece> getResultAndStopTime(Board board, List<Piece> pieces)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        LevelGenerator levelGenerator = new LevelGenerator();
        List<Piece> result = levelGenerator.solveBoard(board, pieces);

        stopwatch.Stop();

        TimeSpan executionTime = stopwatch.Elapsed;
        string formattedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
            executionTime.Hours, executionTime.Minutes, executionTime.Seconds, executionTime.Milliseconds);

        UnityEngine.Debug.Log("Execution Time: " + formattedTime);

        return result;
    }

    public void showBoard(Board board)
    {
        foreach (Piece piece in board.getPieces())
        {
            float rand = getRandColor();
            Color color = new Color(rand, rand, rand);
            foreach (Vector2 offset in piece.getOffsets())
            {
                Vector2 pos = piece.getPos() + offset;
                pos = new Vector2(pos.x, -pos.y);
                GameObject block = Instantiate(blockPrefab, pos, Quaternion.identity);
                block.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    private float getRandColor()
    {
        return (float)UnityEngine.Random.Range(0, 255) / 255f;
    }
}
