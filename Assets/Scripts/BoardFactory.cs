using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardFactory
{
    public Board getTest1Board()
    {
        int[,] slots = {
            { -1, 0, 0, 0 },
            { -1, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
        };
        return new Board(slots);
    }

    public Board getTest2Board()
    {
        int[,] slots = {
            { 0, 0, 0, 0, -1, -1 },
            { -1, -1, 0, 0, 0, 0 },
            { -1, -1, 0, 0, 0, 0 },
            { -1, -1, 0, 0, 0, 0 },
            { -1, -1, -1, -1, 0, -1 },
        };
        return new Board(slots);
    }

    public Board getTest3Board()
    {
        int[,] slots = {
            { -1, 0},
            { 0, 0 },
        };
        return new Board(slots);
    }

    public Board getTest4Board()
    {
        int[,] slots = {
            { -1, -1, -1, 0, 0, -1 },
            { 0, 0, 0, 0, 0, 0 },
            { -1, -1, 0, 0, 0, 0 },
            { -1, -1, -1, 0, 0, 0 },
            { -1, 0, 0, 0, 0, 0 },
            { -1, -1, -1, -1, 0, 0 },
        };
        return new Board(slots);
    }
}
