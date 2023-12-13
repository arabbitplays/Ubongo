using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece
{
    private Vector2[] offsets;
    private Vector2 pos;
    private bool setOnBoard;

    public Piece(Vector2[] offsets)
    {
        this.offsets = offsets;
    }

    public void rotate()
    {
        for(int i = 0; i < offsets.Length; i++)
        {
            Vector2 v = RotateVector(offsets[i], 90f);
            offsets[i] = new Vector2(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        }
    }

    public Vector2[] getOffsets()
    {
        return offsets;
    }

    public Vector2 getPos()
    {
        return pos;
    }

    public void setPos(Vector2 pos)
    {
        this.pos = pos;
    }

    public bool isSetOnBoard()
    {
        return setOnBoard;
    }

    public void setSetOnBoard(bool setOnBoard)
    {
        this.setOnBoard = setOnBoard;
    }

    public Piece copy()
    {
        return new Piece(offsets);
    }

    public void print()
    {
        Debug.Log("Position: " + pos);
        foreach(Vector2 offset in offsets)
        {
            Debug.Log(offset);
        }
    }

    Vector2 RotateVector(Vector2 vector, float angle)
    {
        // Convert the angle to radians
        float radianAngle = Mathf.Deg2Rad * angle;

        // Calculate the rotated coordinates
        float x = vector.x * Mathf.Cos(radianAngle) - vector.y * Mathf.Sin(radianAngle);
        float y = vector.x * Mathf.Sin(radianAngle) + vector.y * Mathf.Cos(radianAngle);

        // Return the rotated vector
        return new Vector2(x, y);
    }
}
